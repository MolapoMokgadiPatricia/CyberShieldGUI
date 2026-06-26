using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberShieldGUI
{
    // Manages the session activity log.
    // Records significant chatbot actions and displays them on request.
    // Shows the last 10 entries by default, with an option to show all.
    public class ActivityLog
    {
        // Stores all activity entries for this session
        private readonly List<ActivityEntry> entries = new List<ActivityEntry>();

        // Maximum entries shown by default before "show more" is needed
        private const int DefaultDisplayCount = 10;

        // Total number of entries recorded so far
        public int Count => entries.Count;

        // Adds a new entry to the log
        public void Add(string description)
        {
            entries.Add(new ActivityEntry(description));
        }

        // Returns a formatted summary of recent activity.
        // If showAll is true, all entries are returned instead of just the last 10.
        public string GetSummary(bool showAll = false)
        {
            if (entries.Count == 0)
                return "No activity recorded yet this session. " +
                       "Start by asking a question, adding a task, or taking the quiz!";

            int displayCount = showAll
                ? entries.Count
                : Math.Min(DefaultDisplayCount, entries.Count);

            var recentEntries = entries
                .TakeLast(displayCount)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine($"Here is a summary of recent actions " +
                          $"(showing {displayCount} of {entries.Count}):\n");

            for (int i = 0; i < recentEntries.Count; i++)
            {
                sb.AppendLine($"  {i + 1}. {recentEntries[i]}");
            }

            if (!showAll && entries.Count > DefaultDisplayCount)
            {
                sb.Append($"\n...and {entries.Count - DefaultDisplayCount} more. " +
                           "Type 'show full log' to see everything.");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
