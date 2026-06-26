using System;
using System.Text.RegularExpressions;

namespace CyberShieldGUI
{
    // Simulates Natural Language Processing using keyword detection,
    // regular expressions, and string.Contains() to identify what the user wants to do.
    // Returns an Intent value so ChatBot can route the request to the correct handler.
    public class NlpProcessor
    {
        // All possible intents the bot can recognise in Part 3
        public enum Intent
        {
            Unknown,
            AddTask,
            ShowTasks,
            CompleteTask,
            DeleteTask,
            SetReminder,
            StartQuiz,
            AnswerQuiz,
            ShowActivityLog,
            ShowFullLog
        }

        // Extracted parameters — populated when an intent is detected
        public string TaskTitle { get; private set; }
        public string TaskDescription { get; private set; }
        public int TaskPosition { get; private set; }    // 1-based position in the task list
        public int ReminderDays { get; private set; }
        public int QuizAnswerIndex { get; private set; } // 0-based: A=0, B=1, C=2, D=3

        // Analyses a normalised (lower-case, trimmed) input string and returns the best intent.
        // Parameters are populated as a side effect.
        public Intent Classify(string input)
        {
            // Quiz answer — single letter A, B, C, or D
            if (Regex.IsMatch(input, @"^[abcd]$"))
            {
                QuizAnswerIndex = input[0] - 'a';
                return Intent.AnswerQuiz;
            }

            // Show full log
            if (input.Contains("full log") || input.Contains("show all") ||
                input.Contains("full history"))
                return Intent.ShowFullLog;

            // Show activity log
            if (input.Contains("activity log") || input.Contains("what have you done") ||
                input.Contains("show log") || input.Contains("recent actions") ||
                input.Contains("history"))
                return Intent.ShowActivityLog;

            // Start quiz
            if (input.Contains("quiz") || input.Contains("start quiz") ||
                input.Contains("test me") || input.Contains("question me") ||
                input.Contains("trivia"))
                return Intent.StartQuiz;

            // Show tasks
            if (input.Contains("show task") || input.Contains("list task") ||
                input.Contains("my task") || input.Contains("view task") ||
                input.Contains("all tasks") || input.Contains("task list"))
                return Intent.ShowTasks;

            // Complete task — e.g. "complete task 2", "mark task 1 as done"
            var completeMatch = Regex.Match(input,
                @"(?:complete|done|finish|mark|tick)\s+task\s+(\d+)");
            if (!completeMatch.Success)
                completeMatch = Regex.Match(input,
                @"task\s+(\d+)\s+(?:done|complete|finished)");

            if (completeMatch.Success)
            {
                TaskPosition = int.Parse(completeMatch.Groups[1].Value);
                return Intent.CompleteTask;
            }

            // Delete task — e.g. "delete task 2", "remove task 1"
            var deleteMatch = Regex.Match(input,
                @"(?:delete|remove|cancel)\s+task\s+(\d+)");

            if (deleteMatch.Success)
            {
                TaskPosition = int.Parse(deleteMatch.Groups[1].Value);
                return Intent.DeleteTask;
            }

            // Set reminder — e.g. "set reminder task 1 in 7 days"
            var reminderMatch = Regex.Match(input,
                @"(?:set\s+reminder|reminder)\s+(?:for\s+)?task\s+(\d+)\s+in\s+(\d+)\s+day");

            if (reminderMatch.Success)
            {
                TaskPosition = int.Parse(reminderMatch.Groups[1].Value);
                ReminderDays = int.Parse(reminderMatch.Groups[2].Value);
                return Intent.SetReminder;
            }

            // "remind me in 3 days" without a task number — applies to most recent task
            var genericReminder = Regex.Match(input,
                @"remind\s+(?:me\s+)?in\s+(\d+)\s+day");

            if (genericReminder.Success)
            {
                ReminderDays = int.Parse(genericReminder.Groups[1].Value);
                TaskPosition = -1; // -1 means apply to the last added task
                return Intent.SetReminder;
            }

            // Add task — various natural phrasings
            bool isAddTask = input.StartsWith("add task") ||
                             input.Contains("add a task") ||
                             input.Contains("create task") ||
                             input.Contains("new task") ||
                             input.StartsWith("remind me to") ||
                             Regex.IsMatch(input, @"add\s+(?:a\s+)?task\s+to\b");

            if (isAddTask)
            {
                ExtractTaskTitle(input);
                return Intent.AddTask;
            }

            return Intent.Unknown;
        }

        // Extracts a clean task title from the user's natural language input.
        private void ExtractTaskTitle(string input)
        {
            // Remove common command prefixes to isolate the task description
            string cleaned = Regex.Replace(input,
                @"^(add\s+(a\s+)?task(\s*:\s*|\s+to\b|\s+)?|create\s+task\s*:?\s*|" +
                @"new\s+task\s*:?\s*|remind\s+me\s+to\s*)",
                "", RegexOptions.IgnoreCase).Trim();

            // Remove trailing reminder info like "in 7 days"
            cleaned = Regex.Replace(cleaned, @"\s+in\s+\d+\s+days?.*$", "").Trim();

            if (string.IsNullOrWhiteSpace(cleaned))
                cleaned = "New Cybersecurity Task";

            // Capitalise first letter
            TaskTitle = char.ToUpper(cleaned[0]) + cleaned.Substring(1);
            TaskDescription = $"Cybersecurity task: {TaskTitle}";
        }
    }
}
