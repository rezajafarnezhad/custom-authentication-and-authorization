using Microsoft.EntityFrameworkCore;
using UserMng.Core.Common;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels.PanelAdmin;
using UserMng.Data.Context;
using UserMng.Data.Entites.Account;

namespace UserMng.Core.Services
{
    public class PanelService : IPanelService
    {

        private readonly ApplicationDatabaseContext _context;

        public PanelService(ApplicationDatabaseContext context)
        {
            _context = context;
        }


        public async Task<ListUsers> GetAllUser(int pageId, int take, string filter)
        {
            try
            {
                var result = _context.Users.Select(c => new UserModel()
                {
                    Id = c.Id.ToString(),
                    Email = c.Email,
                    IsActive = c.IsActive,
                    RegistractionDate = c.RegisterDate,
                    UserName = c.UserName,
                    RoleName = c.userRoles.Select(a=>a.Role.RoleTitle).ToList(),

                });

                if (!string.IsNullOrWhiteSpace(filter))
                    result = result.Where(c => c.UserName == filter || c.Email == filter);

                var skip = (pageId - 1) * take;

                var model = new ListUsers()
                {
                    filter = filter,
                    UserModels = await result.OrderByDescending(c => c.Id).Skip(skip).Take(take).ToListAsync()
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }


        }
        public async Task<List<RoleModel>> GetAllRole()
        {
            return await _context.Roles.Select(c => new RoleModel()
            {
                RoleId = c.Id,
                RoleName = c.RoleTitle
            }).ToListAsync();
        }
        public async Task<OperationResult> AddUser(CreateUser createUser)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = new User()
                {

                    Email = createUser.Email.ToLower(),
                    UserName = createUser.UserName,
                    ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),
                    IsActive = true,
                    Password = HashEncode.GetHashCode(createUser.Password),
                    RegisterDate = DateTimeGenerator.GetShamsiDate(),
                    userRoles = new List<UserRole>()
                };

                if (await _context.Users.AnyAsync(c => c.Email == _user.Email))
                    return operationResult.Failed("ایمیل تکراری است");


                if (await _context.Users.AnyAsync(c => c.UserName == _user.UserName))
                    return operationResult.Failed("نام کاربری تکراری است");

                await _context.Users.AddAsync(_user);
                await _context.SaveChangesAsync();

                await AddRolesToUser(_user.Id, createUser.Roles);
                return operationResult.Succeeded("کاربر با موفقیت ثبت شد");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }
        private async Task AddRolesToUser(int userId, List<int> roles)
        {
            var userRole = new List<UserRole>();

            foreach (var role in roles)
            {
                userRole.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = role
                });
            }

            await _context.UserRoles.AddRangeAsync(userRole);
            await _context.SaveChangesAsync();
        }
        private async Task RemoveRolesToUser(int userId)
        {
            var dateRemove = await _context.UserRoles.Where(c => c.UserId == userId).ToListAsync();
            _context.UserRoles.RemoveRange(dateRemove);
            await _context.SaveChangesAsync();
        }
        public async Task<EditUser> GetForEdit(string userId)
        {
            try
            {
                var _data = await _context.Users.Where(c => c.Id == Convert.ToInt32(userId)).Select(c => new EditUser()
                {
                    Id = c.Id,
                    Email = c.Email,
                    UserName = c.UserName,
                    Roles = c.userRoles.Select(r => r.RoleId).ToList(),

                }).SingleOrDefaultAsync();

                return _data;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<OperationResult> EditUser(EditUser editUser)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var _user = await _context.Users.FindAsync(editUser.Id);

                _user.Email = editUser.Email.ToLower();
                _user.UserName = editUser.UserName;


                _context.Users.Update(_user);
                await _context.SaveChangesAsync();


                //Delete Old UserRoles
                await RemoveRolesToUser(_user.Id);

                //Add Roles
                await AddRolesToUser(_user.Id, editUser.Roles);

                return operationResult.Succeeded("کاربر با موفقیت ویرایش شد");



            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }
        public async Task<ChangeUserPasswordModel> ChangeUserPassword(string userId)
        {
            try
            {
                var _data = await _context.Users.Where(c=>c.Id == Convert.ToInt32(userId)).Select(c => new ChangeUserPasswordModel()
                {
                    Id = c.Id,
                    UserName = c.UserName,

                }).SingleOrDefaultAsync();

                return _data;

            }
            catch (Exception)
            {
                return null;
            }


        }

        public async Task<OperationResult> ChangeUserPassword(ChangeUserPasswordModel changeUserPasswordModel)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var _user = await _context.Users.FindAsync(changeUserPasswordModel.Id);
                _user.Password = HashEncode.GetHashCode(changeUserPasswordModel.Password);
                _context.Users.Update(_user);
                await _context.SaveChangesAsync();
                return operationResult.Succeeded("کلمه عبور با موفقیت تغییر کرد");
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }
        public async Task<OperationResult> ChangeStatus(string Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = await _context.Users.FindAsync(Convert.ToInt32(Id));
                _user.IsActive = !_user.IsActive;
                _context.Users.Update(_user);
                await _context.SaveChangesAsync();
                return operationResult.Succeeded("وضعیت با موفقیت تغییر کرد");
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<List<RoleModel>> GetAllRoleForIndex()
        {
            try
            {
                var _data = await _context.Roles.Select(c => new RoleModel()
                {
                    RoleId = c.Id,
                    RoleName = c.RoleTitle,
                    UserCount = c.userRoles.Select(r=>r.UserId).Count(),

                }).ToListAsync();
                return _data;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public async Task<List<PermissionModel>> GetAllPermission()
        {
            return await _context.Permissions.Select(c => new PermissionModel()
            {
                Id = c.Id,
                ParentPermissionId = c.ParenPermissionId,
                PermissionTitle = c.PermissionTitle

            }).ToListAsync();
        }
        public async Task<OperationResult> CreateRole(RoleModel roleModel)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var _role = new Role()
                {
                    RoleTitle = roleModel.RoleName,

                };

                if (await _context.Roles.AnyAsync(c => c.RoleTitle == _role.RoleTitle))
                    return operationResult.Failed("نام نقش تکراری است");

                await _context.Roles.AddAsync(_role);
                await _context.SaveChangesAsync();
                await AddPermissionToRole(_role.Id, roleModel.Permissions);

                return operationResult.Succeeded("نقش ثبت شد");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }
        private async Task AddPermissionToRole(int RoleId, List<int> Permissions)
        {
            var RolePermissions = new List<RolePermission>();

            foreach (var permissions in Permissions)
            {
                RolePermissions.Add(new RolePermission()
                {
                    RoleId = RoleId,
                    PermissionId = permissions,
                });
            }

            await _context.RolePermissions.AddRangeAsync(RolePermissions);
            await _context.SaveChangesAsync();
        }

        private async Task RemovePermissionRole(int RoleId)
        {
           var _RolePermissons=await _context.RolePermissions.Where(c => c.RoleId == RoleId).ToListAsync();
           _context.RolePermissions.RemoveRange(_RolePermissons);
           await _context.SaveChangesAsync();
        }
        public async Task<RoleModel> GetForEditRole(int Id)
        {
            try
            {
                var _data = await _context.Roles.Where(c=>c.Id == Id).Select(c => new RoleModel()
                {
                    RoleId = c.Id,
                    RoleName = c.RoleTitle,
                    Permissions = c.RolePermissions.Select(r=>r.PermissionId).ToList(),

                }).SingleOrDefaultAsync();

                return _data;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<OperationResult> EditRole(RoleModel roleModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _role = await _context.Roles.FindAsync(roleModel.RoleId);
                _role.RoleTitle = roleModel.RoleName;
                _context.Roles.Update(_role);
                await RemovePermissionRole(_role.Id);
                await AddPermissionToRole(_role.Id,roleModel.Permissions);
                return operationResult.Succeeded("نقش ویرایش شد");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }


        }

        public async Task<OperationResult> RemoveRole(int RoleId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _role = await _context.Roles.FindAsync(RoleId);

                if (await _context.UserRoles.AnyAsync(c => c.RoleId == RoleId))
                    return operationResult.Failed("کاربر یا کاربرانی عضو این نقش هستند");

                _context.Remove(_role);
                await RemovePermissionRole(_role.Id);

                return operationResult.Succeeded("نقش حذف شد");
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }
    }
}
