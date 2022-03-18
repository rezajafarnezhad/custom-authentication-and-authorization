namespace UserMng.Data.Entites.Account
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        public Role Role { get; set; }
        public User user { get; set; }

    }
}
