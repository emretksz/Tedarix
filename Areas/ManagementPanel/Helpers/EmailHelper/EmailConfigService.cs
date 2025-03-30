
namespace Tedarix.Areas.ManagementPanel.Helpers.EmailHelper
{
    using Elfie.Serialization;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Threading.Tasks;
    public class EmailConfigService
    {
        //private readonly EmailConfiguration _emailConfig;

        //public EmailConfigService(EmailConfiguration emailConfig)
        //{
        //    _emailConfig = emailConfig;
        //}

        //public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAddress));
        //    message.To.Add(new MailboxAddress(toEmail,""));
        //    message.Subject = subject;

        //    var bodyBuilder = new BodyBuilder();
        //    bodyBuilder.HtmlBody = htmlMessage;
        //    message.Body = bodyBuilder.ToMessageBody();

        //    using (var client = new SmtpClient())
        //    {
        //        await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
        //        await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
        //        await client.SendAsync(message);
        //        await client.DisconnectAsync(true);
        //    }
        //}
        public async Task<string> SendEmailAsync(EmailConfiguration config, string toEmail, string subject, string htmlMessage, string attachmentFilePath)
        {
            try
            {

                using (var client2 = new SmtpClient())
                {
                    client2.Host = "*";
                    client2.Port = config.Port;
                    client2.EnableSsl = true;
                    client2.Credentials = new NetworkCredential("*", config.SmtpPassword);

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(config.FromName, config.FromAddress);
                  //  msg.To.Add("emretkszz@gmail.com");
                   msg.To.Add("*");
                    msg.Subject = subject;
                    msg.Body = htmlMessage;
                    // Dosyayı eklemek için


                    if (!string.IsNullOrEmpty(attachmentFilePath) && File.Exists(attachmentFilePath))
                    {

                        Attachment attachment = new Attachment(attachmentFilePath);
                        attachment.ContentType = new ContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        msg.Attachments.Add(attachment);

                    }

                    client2.Send(msg);

                   
                }


                return "";
            }
            catch (Exception ex)
            {
                return "";

            }
        }

    }
}