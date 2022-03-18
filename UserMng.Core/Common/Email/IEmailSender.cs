namespace UserMng.Core.Common.Email
{
    public interface IEmailSender
    {
        Task Send(string userEmail, string body, string subject);
    }
}