using Microsoft.AspNetCore.Identity;

namespace HotelBookingAPI.Models.Dto
{
    public class ApplicationUserDto : IdentityUser
    {
        public string Name { get; set; }
    }
}
