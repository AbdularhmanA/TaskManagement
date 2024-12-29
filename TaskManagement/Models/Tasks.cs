using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        public string? UserId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Priority { get; set; }

        public DateTime? Deadline { get; set; } = DateTime.Now;

        public string? RecipientId { get; set; }

        public Users User { get; set; }

        public virtual Users Recipient { get; set; }

        public string? Status { get; set; }

        public string? Track { get; set; }
    }
}
