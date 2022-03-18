using System.ComponentModel.DataAnnotations;

namespace UserMng.Core.ViewModels.PanelAdmin;

public class EditUser
{
    public int Id { get; set; }
    [Required(ErrorMessage = "نام کاربری وارد شود")]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "ایمیل وارد شود")]

    [Display(Name = "ایمیل")]

    public string Email { get; set; }

    public List<int> Roles { get; set; }
}