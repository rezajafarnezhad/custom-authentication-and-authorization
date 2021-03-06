using System.Net;
using System.Net.Mail;
using System.Text;

namespace UserMng.Core.Common.Email
{
    public class EmailSender: IEmailSender
    {
        public async Task Send(string userEmail, string body, string subject)
        {

            SmtpClient client = new SmtpClient();

            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("jafarnezhadzz@gmail.com", "asdfg123##");
            MailMessage message = new MailMessage("jafarnezhadzz@gmail.com", userEmail, subject, body);
            message.From = new MailAddress("jafarnezhadzz@gmail.com", "نرم افزار مدیریت کاربران");
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);
            await Task.CompletedTask;
        }
    }
}
