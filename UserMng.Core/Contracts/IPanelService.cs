using UserMng.Core.Common;
using UserMng.Core.ViewModels.PanelAdmin;

namespace UserMng.Core.Contracts;

public interface IPanelService
{
    Task<ListUsers> GetAllUser(int pageId, int take, string filter);
    Task<List<RoleModel>> GetAllRole();
    Task<OperationResult> AddUser(CreateUser createUser);
    Task<EditUser> GetForEdit(string userId);
    Task<OperationResult> EditUser(EditUser editUser);
    Task<ChangeUserPasswordModel> ChangeUserPassword(string userId);
    Task<OperationResult> ChangeUserPassword(ChangeUserPasswordModel changeUserPasswordModel);
    Task<OperationResult> ChangeStatus(string Id);
    Task<List<RoleModel>> GetAllRoleForIndex();
    Task<List<PermissionModel>> GetAllPermission();
    Task<OperationResult> CreateRole(RoleModel roleModel);
    Task<RoleModel> GetForEditRole(int Id);
    Task<OperationResult> EditRole(RoleModel roleModel);
    Task<OperationResult> RemoveRole(int RoleId);
    bool CheckPermission(int PermissionId, string userName);
}