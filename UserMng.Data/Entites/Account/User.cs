namespace UserMng.Data.Entites.Account
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public string RegisterDate { get; set; }

        public ICollection<UserRole> userRoles { get; set; }

    }
}
