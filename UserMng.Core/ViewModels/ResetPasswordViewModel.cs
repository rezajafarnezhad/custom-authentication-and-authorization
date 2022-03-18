using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور وارد شود")]

        public string Password { get; set; }

        [Required(ErrorMessage = "تایید کلمه عبور وارد شود")]
        [Display(Name = "تایید کلمه عبور")]
        [Compare(nameof(Password), ErrorMessage = "تایید کلمه عبور درست وارد شود")]
        public string ConfirmPassword { get; set; }

    }
}
