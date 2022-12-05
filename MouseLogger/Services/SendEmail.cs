using System.Windows;
using MimeKit;
using System.Text;

namespace MouseLogger.Services
{
    public class SendEmail
    {
        public void Send(string mail, string mailTo)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Name", "Email")); // Данные для отправки
                message.To.Add(new MailboxAddress(mailTo, mailTo));
                message.Subject = "Отчет";

                var bodyBuilder = new BodyBuilder();

                StringBuilder body = new StringBuilder();

                message.Body = new BodyBuilder()
                {
                    HtmlBody = mail
                }.ToMessageBody();

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("Email", "Password"); // Данные для отправки
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка с отправкой на почту. Настройте файл SendEmail");
            }
        }
    }
}
