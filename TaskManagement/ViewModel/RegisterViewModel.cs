using System.ComponentModel.DataAnnotations;

namespace TaskManagement.ViewModel
{
    public class RegisterViewModel
    {

        // The employee number for the user, used as the primary key (Key)
        [Key]
        [Display(Name = "الرقم الوظيفي")]
        [Required(ErrorMessage = "الرقم الوظيفي مطلوب.")] // Ensure the employee number is required
        public string UserID { get; set; }

        // The full name of the employee
        [Display(Name = "اسم الموظف")]
        [Required(ErrorMessage = "اسم الموظف مطلوب.")] // Ensure the employee's full name is required
        public string FullName { get; set; }

        // The job title, which is a required field
        [Required(ErrorMessage = "المسمى الوظيفي مطلوب.")]
        [Display(Name = "المسمى الوظيفي")]
        public string jobtitle { get; set; }

        // The job position, which is a required field
        [Display(Name = "المنصب الوظيفي")]
        [Required(ErrorMessage = "المنصب الوظيفي مطلوب")] // Ensure the job position is required
        public string JobPosition { get; set; }

        // The user's email, which must be in the correct format
        [Display(Name = "البريد الإلكتروني")]
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")] // Ensure the email is required
        [EmailAddress] // Ensure the email entered is valid
        public string Email { get; set; }

        // The password, which must be between 8 and 15 characters
        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage = "كلمة المرور مطلوبة.")] // Ensure the password is required
        [StringLength(15, MinimumLength = 8, ErrorMessage = "كلمة المرور يجب أن تكون من 8 إلى 15 رموز")] // Set the password length
        [DataType(DataType.Password)] // Display the field as dots for security
        [Compare("ConfirmPassword", ErrorMessage = "كلمة المرور لا تتطابق.")] // Ensure the password matches the confirmation
        public string Password { get; set; }

        // Confirm password to ensure it was entered correctly
        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب.")] // Ensure the confirm password is required
        [DataType(DataType.Password)] // Display the field as dots for security
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }
    }
}
