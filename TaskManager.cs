using System;
using System.Collections.Generic;
using System.Text;

namespace CyberShieldGUI
{
    // Manages the user's cybersecurity task list.
    // Keeps an in-memory list and syncs with MySQL via DatabaseService.
    // If the database is unavailable, the app continues in memory-only mode.
    public class TaskManager
    {
        private readonly List<CyberTask> tasks = new List<CyberTask>();
        private readonly DatabaseService db = new DatabaseService();
        private readonly ActivityLog activityLog;
        private bool dbAvailable;

        public IReadOnlyList<CyberTask> Tasks => tasks;

        public TaskManager(ActivityLog log)
        {
            activityLog = log;
            dbAvailable = db.TestConnection();

            if (dbAvailable)
            {
                try { tasks.AddRange(db.GetTasks()); }
                catch { dbAvailable = false; }
            }
        }

        // Adds a new task and saves it to the database if available.
        // Returns a confirmation message for the chat window.
        public string AddTask(string title, string description, DateTime? reminderDate = null)
        {
            var task = new CyberTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            if (dbAvailable)
            {
                try
                {
                    // FIX 1: Store the returned ID back so CompleteTask
                    // and DeleteTask can update the correct DB row
                    task.Id = db.AddTask(task);
                }
                catch
                {
                    dbAvailable = false;
                }
            }

            tasks.Add(task);
            activityLog.Add($"Task added: '{title}'" +
                (reminderDate.HasValue ? $" (reminder: {reminderDate.Value:dd MMM yyyy})" : ""));

            string reminderLine = reminderDate.HasValue
                ? $"\n  Reminder set for: {reminderDate.Value:dd MMM yyyy}"
                : "";

            return $"Task added successfully!\n\n" +
                   $"  Title:       {title}\n" +
                   $"  Description: {description}" +
                   reminderLine +
                   "\n\nWould you like to set a reminder for this task? (yes / no)";
        }

        // Returns a formatted list of all tasks for display in the chat window.
        public string GetTaskListMessage()
        {
            if (tasks.Count == 0)
                return "You have no tasks yet. You can add one by typing:\n" +
                       "'add task: Enable two-factor authentication'";

            var sb = new StringBuilder();
            sb.AppendLine($"Your Cybersecurity Task List ({tasks.Count} task(s)):\n");

            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "Completed" : "Pending";
                string reminder = tasks[i].ReminderDate.HasValue
                    ? $"  |  Reminder: {tasks[i].ReminderDate.Value:dd MMM yyyy}"
                    : "";

                sb.AppendLine($"  {i + 1}. [{status}]  {tasks[i].Title}{reminder}");

                if (!string.IsNullOrWhiteSpace(tasks[i].Description))
                    sb.AppendLine($"       {tasks[i].Description}");
            }

            sb.Append("\nTo manage tasks type: 'complete task 1', 'delete task 2', " +
                      "'set reminder task 1 in 7 days'");
            return sb.ToString();
        }

        // Marks the task at the given 1-based position as completed.
        public string CompleteTask(int position)
        {
            if (!TryGetByPosition(position, out CyberTask task))
                return $"I could not find task {position}. Type 'show tasks' to see your list.";

            if (task.IsCompleted)
                return $"Task '{task.Title}' is already marked as completed.";

            task.IsCompleted = true;

            if (dbAvailable && task.Id > 0)
            {
                try { db.MarkTaskCompleted(task.Id); }
                catch { dbAvailable = false; }
            }

            activityLog.Add($"Task completed: '{task.Title}'");
            return $"Great work! Task '{task.Title}' has been marked as completed.";
        }

        // Deletes the task at the given 1-based position.
        public string DeleteTask(int position)
        {
            if (!TryGetByPosition(position, out CyberTask task))
                return $"I could not find task {position}. Type 'show tasks' to see your list.";

            if (dbAvailable && task.Id > 0)
            {
                try { db.DeleteTask(task.Id); }
                catch { dbAvailable = false; }
            }

            tasks.Remove(task);
            activityLog.Add($"Task deleted: '{task.Title}'");
            return $"Task '{task.Title}' has been deleted.";
        }

        // Sets a reminder on the task at the given 1-based position.
        public string SetReminder(int position, DateTime reminderDate)
        {
            if (!TryGetByPosition(position, out CyberTask task))
                return $"I could not find task {position}. Type 'show tasks' to see your list.";

            task.ReminderDate = reminderDate;

            // FIX 2: Now calls UpdateReminder to persist the date to the database
            if (dbAvailable && task.Id > 0)
            {
                try { db.UpdateReminder(task.Id, reminderDate); }
                catch { dbAvailable = false; }
            }

            activityLog.Add($"Reminder set for '{task.Title}' on {reminderDate:dd MMM yyyy}");
            return $"Got it! Reminder set for '{task.Title}' on {reminderDate:dd MMM yyyy}.";
        }

        // Helper: gets a task by 1-based position from the list
        private bool TryGetByPosition(int position, out CyberTask task)
        {
            task = null;
            if (position < 1 || position > tasks.Count) return false;
            task = tasks[position - 1];
            return true;
        }
    }
}
