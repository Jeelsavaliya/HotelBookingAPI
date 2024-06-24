using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPI.Models.Dto
{
    public class CheckAvailabilityDto
    {
        public int CheckAvailabilityID { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
