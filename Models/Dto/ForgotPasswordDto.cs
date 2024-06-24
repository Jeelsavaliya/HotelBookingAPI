using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models.Dto
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage ="Username is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="New Password is required")]
        public string Password { get; set; } 
    }
}
