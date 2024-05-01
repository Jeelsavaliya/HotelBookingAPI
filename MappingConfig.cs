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
            });
            return mappingConfig;
        }
    }
}
