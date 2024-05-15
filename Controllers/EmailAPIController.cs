using HotelBookingAPI.Models;
using HotelBookingAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailAPIController : ControllerBase
    {
        private readonly IMailService _mailService;
        //injecting the IMailService into the constructor
        public EmailAPIController(IMailService _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMail")]
        public bool SendMail(MailData mailData)
        {
            return _mailService.SendMail(mailData);
        }

        /*[HttpPost]
        public IActionResult SendMail(string body) 
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tanner4@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("tanner4@ethereal.email"));
            email.Subject = "Text Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect
                smtp.Authenticate
                smtp.Send
                smtp.Dis

        }*/
    }
}
