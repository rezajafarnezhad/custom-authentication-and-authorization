namespace UserMng.Core.ViewModels.PanelAdmin
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserCount { get; set; }
        public List<int> Permissions { get; set; }
    }
}
