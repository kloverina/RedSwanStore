using System;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace RedSwanStore.Utils
{
    public class EmailService
    {
        public string AdminName { get; set; } = "Red Swan Store";
        public string AdminEmail { get; set; } = "redswanstore@yandex.ru";
        public string AdminPassword { get; set; } = "redswan";
        public string EmailServer { get; set; } = "smtp.yandex.ru";
        
        public string SendPasswordRecoveryEmail(string email, string name, string surname, string newPassword)
        {
            try
            {
                var emailMessage = new MimeMessage();
            
                emailMessage.From.Add(new MailboxAddress(AdminName, AdminEmail));
                emailMessage.To.Add(new MailboxAddress($"{name} {surname}", email));
                emailMessage.Subject = "Восстановление пароля Red Swan Store.";
                emailMessage.Body = new TextPart(TextFormat.Html) {
                    Text = "<h2 style=\"font-size:32px;line-height:36px;font-weight:500;padding-bottom:10px;color:#333;text-align:center\">" +
                           $"Здравствуйте, {name} {surname}," +
                           "</h2>" +
                           "<div style=\"font-size:17px;line-height:25px;color:#333;font-weight:normal\">" +
                           $"<p>Пароль Вашей учетной записи Red Swan Store {email} был успешно сброшен.</p>" +
                           $"<p>Ваш новый временный пароль: {newPassword}. Мы настоятельно рекомендуем Вам как можно скорее сменить этот пароль на придуманный Вами.</p>" +
                           "<p>Если Вы не сбрасывали пароль или считаете, что посторонние лица получили доступ к Вашей учетной записи, немедленно обратитесь в техническую" +
                           " поддержку или к администрации Red Swan Store.</p>" +
                           "<p>С уважением,</p>" +
                           "<p>Администрация Red Swan Store</p>"
                };


                using (var client = new SmtpClient())
                {
                    client.Connect(EmailServer, 25, false);
                    client.Authenticate(AdminEmail, AdminPassword);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
        }
    }
}