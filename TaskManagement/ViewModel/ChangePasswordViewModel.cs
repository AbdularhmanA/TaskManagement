using System.ComponentModel.DataAnnotations;

namespace TaskManagement.ViewModel
{
    public class ChangePasswordViewModel
    {
        
        [Display(Name = "البريد الإلكتروني")]
        [Required(ErrorMessage = "البريد الالكتروني مطلوب")]  
        [EmailAddress]  
        public string Email { get; set; }

        
        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]  
        [StringLength(15, MinimumLength = 8, ErrorMessage = "كلمة المرور الجديدة يجب ان تتكون من 8 الى 15 رمز")]  
        [DataType(DataType.Password)]  
        [Display(Name = "كلمة المرور الجديدة")]
        [Compare("ConfirmNewPassword", ErrorMessage = "كلمة المرور لاتتطابق.")]  
        public string NewPassword { get; set; }

        
        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب.")]  
        [DataType(DataType.Password)]  
        [Display(Name = "تأكيد كلمة المرور الجديدة")]
        public string ConfirmNewPassword { get; set; }
    }
}
