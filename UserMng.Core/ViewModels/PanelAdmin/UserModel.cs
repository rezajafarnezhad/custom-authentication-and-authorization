using UserMng.Core.Common;

namespace UserMng.Core.ViewModels.PanelAdmin
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RegistractionDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> RoleName { get; set; }
    }

    public class ListUsers :BasePaging
    {
        public List<UserModel> UserModels { get; set; }
        public string filter { get; set; }

    }
}
