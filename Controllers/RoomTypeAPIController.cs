using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using HotelBookingAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public RoomTypeAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<RoomType> objList = _db.RoomTypes.ToList();
                _response.Result = _mapper.Map<IEnumerable<RoomTypeDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                RoomType obj = _db.RoomTypes.First(u => u.RoomTypeID == id);
                _response.Result = _mapper.Map<RoomTypeDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] RoomTypeDto roomTypeDto)
        {
            try
            {
                RoomType obj = _mapper.Map<RoomType>(roomTypeDto);
                _db.RoomTypes.Add(obj);
                _db.SaveChanges();

                if (roomTypeDto.Photo != null)
                {
                    string FilePath = "wwwroot\\RoomTypeImages";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileNameWithPath = Path.Combine(path, roomTypeDto.File.FileName);
                    roomTypeDto.Photo = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + roomTypeDto.File.FileName;

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(stream);
                    }
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] RoomTypeDto roomTypeDto)
        {
            try
            {
                RoomType obj = _mapper.Map<RoomType>(roomTypeDto);
                _db.RoomTypes.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<RoomTypeDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                RoomType obj = _db.RoomTypes.First(u => u.RoomTypeID == id);
                _db.RoomTypes.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
