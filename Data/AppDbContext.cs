using HotelBookingAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //RoomTypeAPI
        public DbSet<RoomType> RoomTypes { get; set; }

        //RoomTypeAPI
        public DbSet<Room> Rooms { get; set; }

        //BookingRoomAPI
        public DbSet<BookingRoom> BookingRooms { get; set; }

        //CheckAvailabilityAPI
        public DbSet<CheckAvailability> CheckAvailabilitys { get; set; }

        //AuthAPI
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }
    }
}
