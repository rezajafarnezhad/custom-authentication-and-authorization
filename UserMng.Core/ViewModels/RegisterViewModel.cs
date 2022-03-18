using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری وارد شود")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ایمیل وارد شود")]

        [Display(Name = "ایمیل")]

        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور وارد شود")]

        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تایید کلمه عبور وارد شود")]

        [Display(Name = "تایید کلمه عبور")]
        [Compare(nameof(Password), ErrorMessage = "تایید کلمه عبور درست وارد شود")]
        public string ConfirmPassword { get; set; }

    }
}
