namespace UserMng.Data.Entites.Account
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionTitle { get; set; }
        public int? ParenPermissionId { get; set; }
        public Permission permission { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
