using AutoMapper;
using HotelBookingAPI.Models;
using HotelBookingAPI.Models.Dto;

namespace HotelBookingAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RoomTypeDto, RoomType>().ReverseMap();
                config.CreateMap<RoomDto, Room>().ReverseMap();
                config.CreateMap<CheckAvailabilityDto, CheckAvailability>().ReverseMap();
                config.CreateMap<BookingRoomDto, BookingRoom>().ReverseMap();
                config.CreateMap<CheckAvailabilityDto, CheckAvailability>().ReverseMap();
                config.CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
                config.CreateMap<UserDto, ApplicationUser>().ReverseMap();
                              
            });
            return mappingConfig;
        }
    }
}
