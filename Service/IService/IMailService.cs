using HotelBookingAPI.Models;

namespace HotelBookingAPI.Service.IService
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
        /*bool SendHTMLMail(HTMLMailData htmlMailData);*/
    }
}


