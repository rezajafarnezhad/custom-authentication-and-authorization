using UserMng.Core.Common;
using UserMng.Core.ViewModels;
using UserMng.Data.Entites.Account;

namespace UserMng.Core.Contracts
{
    public interface IuserService
    {
        Task<bool> EmailExists(string email);
        Task<bool> UserNameExists(string userName);
        Task<OperationResult> Register(RegisterViewModel register);
        Task<bool> IsExistsUser(string email);
        Task<OperationResult> Login(LoginViewModel loginViewModel);
        Task<OperationResult> ActiveAccount(string ActiveCode);
        Task<User> GetUserBy(string userId);
        Task<OperationResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel);
        Task<ResetPasswordViewModel> ResetPassword(string ActiveCode);
        Task<OperationResult> ResetPasswordStep2(ResetPasswordViewModel passwordViewModel);
    }
}
