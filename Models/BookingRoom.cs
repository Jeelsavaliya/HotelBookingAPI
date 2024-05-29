using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPI.Models
{
    public class BookingRoom
    {
        [Key] 
        public int BookingRoomID { get; set; }
        public int RoomID { get; set; }

        public string? UserId { get; set; }

        /*public Room Image { get; set; }*/
        [Column("FirstName", TypeName = "nvarchar(50)")]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column("Email", TypeName = "nvarchar(50)")]
        [Required]
        public string Email { get; set; }
        [Column("PhoneNumber", TypeName = "nvarchar(50)")]
        [Required]
        public string PhoneNumber { get; set; }
        [Column("Address", TypeName = "nvarchar(255)")]
        [Required]
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckInDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOutDate { get; set;}

        public decimal? TotalPrice { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
