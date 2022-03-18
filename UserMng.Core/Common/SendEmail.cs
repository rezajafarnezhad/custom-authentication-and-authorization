using System.Net.Mail;

namespace UserMng.Core.Common
{

    public static class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("jafarnezhadzz@gmail.com", "نرم افزار مدیریت کاربران");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("jafarnezhadzz@gmail.com", "asdfg123##");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }


}
