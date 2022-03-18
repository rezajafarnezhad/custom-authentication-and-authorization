using Microsoft.EntityFrameworkCore;
using UserMng.Core.Common;
using UserMng.Core.Contracts;
using UserMng.Core.ViewModels;
using UserMng.Data.Context;
using UserMng.Data.Entites.Account;

namespace UserMng.Core.Services
{
    public class UserServices : IuserService
    {
        private readonly ApplicationDatabaseContext _context;

        public UserServices(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(c => c.Email == email);
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await _context.Users.AnyAsync(c => c.UserName == userName);
        }

        public async Task<OperationResult> Register(RegisterViewModel register)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = new User()
                {
                    UserName = register.UserName,
                    Email = register.Email.ToLower(),
                    IsActive = false,
                    RegisterDate = DateTimeGenerator.GetShamsiDate(),
                    Password = HashEncode.GetHashCode(register.Password),
                    ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),

                };

                if (await UserNameExists(_user.UserName))
                    return operationResult.Failed("نام کاربری دیگری انتخاب کنید");

                if (await EmailExists(_user.Email))
                    return operationResult.Failed("ایمیل دیگری انتخاب کنید");

                await _context.Users.AddAsync(_user);
                await _context.SaveChangesAsync();
                return operationResult.Succeeded($"لینک فعال سازی  حساب به ایمیل شما ارسال شد",
                    $"{_user.Email}`{_user.ActiveCode}");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }


        }

        public async Task<bool> IsExistsUser(string email)
        {
            return await _context.Users.AnyAsync(c => c.Email == email);
        }

        public async Task<OperationResult> Login(LoginViewModel loginViewModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _email = loginViewModel.Email.ToLower();
                var _password = HashEncode.GetHashCode(loginViewModel.Password);

                var _userId = await _context.Users
                    .Where(c => c.Password == _password && c.Email == _email && c.IsActive).Select(c => c.Id.ToString())
                    .SingleOrDefaultAsync();

                if (_userId is null)
                    return operationResult.Failed("از صحت اطلاعات وارد شده اصمینان حاصل کنید.");


                return operationResult.Succeeded("ورود با موفقیت انجام شد.روی پنل کلیک کنید", _userId);



            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OperationResult> ActiveAccount(string ActiveCode)
        {
            OperationResult operationResult = new OperationResult();
            var _user = await _context.Users.SingleOrDefaultAsync(c => c.ActiveCode == ActiveCode);
            if (_user is null)
            {
                return operationResult.Failed();
            }
            else
            {
                _user.IsActive = true;
                _user.ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
                _context.Users.Update(_user);
                await _context.SaveChangesAsync();
                return operationResult.Succeeded("");
            }

        }
        public async Task<User> GetUserBy(string userId)
        {
            try
            {
                var _user = await _context.Users.FindAsync(Convert.ToInt32(userId));
                return _user;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var _user = await _context.Users.Where(c => c.Email == forgotPasswordViewModel.Email.ToLower())
                    .SingleOrDefaultAsync();

                if (_user is null)
                    return operationResult.Failed("از صحت ایمیل وارد شده مطمئن شوید");

                return operationResult.Succeeded("لینک بازیابی کلمه عبور به ایمیل شما ارسال شد",
                    $"{_user.UserName}`{_user.ActiveCode}");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }


        public async Task<ResetPasswordViewModel> ResetPassword(string ActiveCode)
        {
            try
            {
                var _user = await _context.Users.Where(c => c.ActiveCode == ActiveCode).Select(c =>
                    new ResetPasswordViewModel()
                    {
                        UserId = c.Id,
                        Password = "",
                        ConfirmPassword = "",
                    }).SingleOrDefaultAsync();

                return _user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> ResetPasswordStep2(ResetPasswordViewModel passwordViewModel)
        {

            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = await _context.Users.FindAsync(passwordViewModel.UserId);

                _user.Password = HashEncode.GetHashCode(passwordViewModel.Password);
                _user.ActiveCode = Guid.NewGuid().ToString().Replace("-", "");

                _context.Users.Update(_user);
                await _context.SaveChangesAsync();

                return operationResult.Succeeded("کلمه عبور شما با موفقیت تغییر کرد");

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }


        }
    }
}
