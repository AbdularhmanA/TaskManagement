namespace TaskManagement.ViewModel
{
    public class TaskListViewModel
    {
        public int TaskId { get; set; } // Unique identifier for the task

        public string RecipientFullName { get; set; } // Recipient's full name
        public string Title { get; set; } // Task title
        public string Priority { get; set; } // Execution speed
        public DateTime? Deadline { get; set; } // Deadline
        public string Status { get; set; } // Task status
    }
}
