namespace UserMng.Core.ViewModels.PanelAdmin
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public int? ParentPermissionId { get; set; }
        public string PermissionTitle { get; set; }

    }
}
