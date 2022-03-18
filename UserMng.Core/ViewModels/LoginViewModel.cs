using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ایمیل وارد شود")]

        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور وارد شود")]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; }

    }
}