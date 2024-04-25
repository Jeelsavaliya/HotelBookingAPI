using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeID { get; set; }

        [Column("RoomName", TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        [Column("Photo", TypeName = "nvarchar(500)")]
        public string Photo { get; set; } = String.Empty;

        [Column("Discription", TypeName = "nvarchar(255)")]

        public string Discription { get; set; }

        [Column("Services", TypeName = "nvarchar(255)")]
        public string Services { get; set; }

        [Column("Size", TypeName = "decimal(10,2)")]
        public decimal Size { get; set; }

        [Column("Capacity", TypeName = "int")]
        public int Capacity { get; set; }
    }
}
