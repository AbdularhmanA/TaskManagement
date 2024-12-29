using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Models
{
    public class Users : IdentityUser
    {
        public string ?FullName { get; set; }
        public string ?JobTitle { get; set; }
        public string ?JobPosition { get; set; }

    }
}
