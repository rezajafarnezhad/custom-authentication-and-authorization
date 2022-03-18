namespace UserMng.Data.Entites.Account
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; }
        public ICollection<UserRole> userRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }


    }
}
