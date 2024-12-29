namespace TaskManagement.ViewModel
{
    public class TrackTaskViewModel
    {
        public int TaskId { get; set; } // Task identifier

        public string Title { get; set; } // Task title

        public string? Description { get; set; } // Task description (can be added if needed)

        public string Priority { get; set; } // Execution speed or priority (e.g., high, medium, low)

        public DateTime? Deadline { get; set; } // Deadline for submission

        public string? Track { get; set; } // Task status (example completed or not completed)

        public string? UpdatedStatus { get; set; } // The new status to be updated
    }
}
