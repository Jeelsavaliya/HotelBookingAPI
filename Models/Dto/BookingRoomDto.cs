﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPI.Models.Dto
{
    public class BookingRoomDto
    {
        public int BookingRoomID { get; set; }
        public int RoomID { get; set; }

        public string? UserId { get; set; }

        /* public Room Image {  get; set; }*/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public string? Payment { get; set; }
    }

}
