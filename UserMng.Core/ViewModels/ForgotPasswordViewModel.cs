using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels;

public class ForgotPasswordViewModel
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "ایمیل وارد شود")]
    public string Email { get; set; }
}