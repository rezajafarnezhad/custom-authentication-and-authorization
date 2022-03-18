using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels.PanelAdmin
{
    public class ChangeUserPasswordModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        
        
        
        [Required(ErrorMessage = "کلمه عبور وارد شود")]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تایید کلمه عبور وارد شود")]
        [Display(Name = "تایید کلمه عبور")]
        [Compare(nameof(Password), ErrorMessage = "تایید کلمه عبور درست وارد شود")]
        public string ConfirmPassword { get; set; }
    }
}
