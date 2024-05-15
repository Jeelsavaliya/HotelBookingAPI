using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models.Dto;
using HotelBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public BookingRoomAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        #region Get All BookingRooms
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<BookingRoom> objList = _db.BookingRooms.ToList();
                _response.Result = _mapper.Map<IEnumerable<BookingRoomDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Get BookingRoom
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                BookingRoom obj = _db.BookingRooms.First(u => u.BookingRoomID == id);
                _response.Result = _mapper.Map<BookingRoomDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Create BookingRooms
        [HttpPost]
        public ResponseDto Post([FromBody] BookingRoomDto bookingRoomDto)
        {
            try
            {
                BookingRoom bookingRoom = _mapper.Map<BookingRoom>(bookingRoomDto);
                _db.BookingRooms.Add(bookingRoom);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update BookingRoom
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] BookingRoomDto bookingRoomDto)
        {
            try
            {
                BookingRoom bookingRoom = _mapper.Map<BookingRoom>(bookingRoomDto);
                _db.BookingRooms.Update(bookingRoom);
                _db.SaveChanges();

                _response.Result = _mapper.Map<BookingRoomDto>(bookingRoom);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete BookingRoom
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                BookingRoom obj = _db.BookingRooms.First(u => u.BookingRoomID == id);
                _db.BookingRooms.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
    }
}
