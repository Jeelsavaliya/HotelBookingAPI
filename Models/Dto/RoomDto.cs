﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models.Dto
{
    public class RoomDto
    {
        public int RoomID { get; set; }
        public int RoomTypeID { get; set; }
        public string Status { get; set; }
        public IFormFile? File { get; set; }
        public string? Image { get; set; } = String.Empty;
        public string Discription { get; set; }
        public int Capacity { get; set; }
        public int RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        [NotMapped]
        public RoomTypeDto? RoomType { get; set; }
    }
}
