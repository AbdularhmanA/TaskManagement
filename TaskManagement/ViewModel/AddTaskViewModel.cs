using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagement.ViewModel
{
    public class AddTaskViewModel
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime? Deadline { get; set; } = DateTime.Now;
        public string Priority { get; set; } 
        public string SelectedRecipientId { get; set; } 
        public List<SelectListItem> AvailableRecipients { get; set; } = new List<SelectListItem>(); 
    }
}
