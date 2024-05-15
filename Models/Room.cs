using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        
        public int RoomTypeID { get; set; }
        [Column("Status", TypeName = "nvarchar(50)")]
        [Required]
        public string Status { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        [Column("Photo", TypeName = "nvarchar(500)")]
        public string? Image { get; set; } = String.Empty;
        [Column("Discription", TypeName = "nvarchar(255)")]
        public string Discription { get; set; }
        [Column("Capacity", TypeName = "int")]
        public int Capacity { get; set; }
        [Column("RoomNumber", TypeName = "int")]
        public int RoomNumber { get; set; }

        /*public decimal PricePerNight { get; set; }*/

        //For ForeignKey
        [ForeignKey("RoomTypeID")]
        public RoomType RoomType { get; set; }
        //public IEnumerable<RoomType> RoomTypeList { get; set; }

    }

     
}
