namespace UserMng.Web.Authentication
{
    public class Roles
    {

        public const int CanViewAdminPanel = 1;

        #region User Manager

        public const int CanViewUserManager = 2;
        public const int CanViewAddUser = 3;
        public const int CanViewEditUser = 4;
        public const int CanViewChangeStatus = 5;
        public const int CanViewChangePassword = 10;

        #endregion 
        
        #region User Manager

        public const int CanViewRoleManager = 6;
        public const int CanViewAddRole = 7;
        public const int CanViewEditRole = 8;
        public const int CanViewRemoveRole = 9;

        #endregion

    }
}
