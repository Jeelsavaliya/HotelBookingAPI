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
    public class RoomAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public RoomAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        #region Get All Rooms
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Room> objList = _db.Rooms.ToList();
                _response.Result = _mapper.Map<IEnumerable<RoomDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Gett Room
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Room obj = _db.Rooms.First(u => u.RoomID == id);
                _response.Result = _mapper.Map<RoomDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Create Rooms
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromForm] RoomDto roomDto)
        {
            try
            {
                Room room = _mapper.Map<Room>(roomDto);
                _db.Rooms.Add(room);
                _db.SaveChanges();

                if (roomDto.File != null)
                {
                    if (!string.IsNullOrEmpty(room.Image))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), room.Image);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = room.RoomID + Path.GetExtension(roomDto.File.FileName);
                    string filePath = @"wwwroot\RoomImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        roomDto.File.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    room.Image = "/RoomImages/" + fileName;

                }
                else
                {
                    room.Image = "https://placehold.co/600x400";
                }
                _db.Rooms.Update(room);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RoomDto>(room);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update Room
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromForm] RoomDto roomDto)
        {
            try
            {
                Room room = _mapper.Map<Room>(roomDto);
                if (roomDto.File != null)
                {
                    if (!string.IsNullOrEmpty(room.Image))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), room.Image);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = room.RoomID + Path.GetExtension(roomDto.File.FileName);
                    string filePath = @"wwwroot\RoomImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        roomDto.File.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    room.Image = "/RoomImages/" + fileName;
                }
                _db.Rooms.Update(room);
                _db.SaveChanges();

                _response.Result = _mapper.Map<RoomDto>(room);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete Room
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Room obj = _db.Rooms.First(u => u.RoomID == id);
                if (!string.IsNullOrEmpty(obj.Image))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.Image);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _db.Rooms.Remove(obj);
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
