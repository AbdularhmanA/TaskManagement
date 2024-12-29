namespace TaskManagement.ViewModel
{
    public class TaskReceptionViewModel
    {
        public int TaskId { get; set; } // Adding task identifier to make it easier to recognize
        public string SenderFullName { get; set; } // Sender's name (not present in Tasks, but required here for display)
        public string Title { get; set; } // Task title
        public string Description { get; set; } // Task description
        public string Priority { get; set; } // Execution speed or priority
        public DateTime? Deadline { get; set; } // Deadline for submission
    }
}
