using System;

namespace CyberShieldGUI
{
    // Represents a single recorded action in the session activity log.
    // Each entry stores a timestamp and a short description of what happened.
    public class ActivityEntry
    {
        public DateTime Timestamp { get; }
        public string Description { get; }

        public ActivityEntry(string description)
        {
            Timestamp = DateTime.Now;
            Description = description;
        }

        // Returns a formatted string for display in the chat window
        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}]  {Description}";
        }
    }
}
