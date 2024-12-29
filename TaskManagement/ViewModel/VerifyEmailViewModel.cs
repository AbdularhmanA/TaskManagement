using System.ComponentModel.DataAnnotations;

namespace TaskManagement.ViewModel
{
    public class VerifyEmailViewModel
    {
        // The email to be verified
        [Display(Name = "Email")] // The field name displayed to the user
        [Required(ErrorMessage = "Email is required.")] // Ensure that the email is required
        [EmailAddress] // Ensure that the entered email is in the correct format (e.g., name@example.com)
        public string Email { get; set; }
    }
}
