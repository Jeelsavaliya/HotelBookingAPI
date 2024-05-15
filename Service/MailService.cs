using HotelBookingAPI.Models;
using HotelBookingAPI.Service.IService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HotelBookingAPI.Service
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }


        bool IMailService.SendMail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    //MailboxAddress emailFrom = new MailboxAddress("Admin", "admin@gmail.com");
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToID);
                    emailMessage.To.Add(emailTo);

                   /* emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));*/

                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        
                        mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                        //mailClient.Authenticate("admin@gmail.com","Admin123*");
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }
    }
}
