using System.ComponentModel.DataAnnotations;

namespace TaskManagement.ViewModel
{
    public class LoginViewModel
    {
        // The email used for login, validated and made required
        [Display(Name = "البريد الإلكتروني")]
        [Required(ErrorMessage = "البريد الالكتروني مطلوب")]  // Ensure this field is required
        [EmailAddress]  // Validate the entered email format
        public string Email { get; set; }

        // The password required for login, displayed as dots for security
        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]  // Ensure the password is required
        [DataType(DataType.Password)]  // Display the field content as dots for security
        public string Password { get; set; }

        // Option to remember the user after login, displayed as a checkbox
        [Display(Name = "تذكرني؟")]
        public bool RememberMe { get; set; }
    }
}
