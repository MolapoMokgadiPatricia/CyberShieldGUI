using System;
namespace CyberShieldGUI
{
    public class CyberTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReminderDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}