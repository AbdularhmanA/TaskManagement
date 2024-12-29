using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    
    public class TaskMDbContext : IdentityDbContext<Users>
    {
      
        public TaskMDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Tasks> Tasks { get; set; }

    }
}
