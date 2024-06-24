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
        private readonly IConfiguration _configuration;


        public RoomTypeAPIController(AppDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
            _configuration = configuration;
        }

        #region Get All RoomType 
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<RoomType> objList = _db.RoomTypes.ToList();

                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Get Roomtype
        [HttpGet("{id}")]
        public ResponseDto Get([FromRoute] int id)
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
        #endregion

        #region Create RoomType
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromForm] RoomTypeDto roomTypeDto)
        {
            try
            {
                RoomType roomType = _mapper.Map<RoomType>(roomTypeDto);

                if (roomTypeDto.File != null)
                {
                    if (!string.IsNullOrEmpty(roomType.Image))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), roomType.Image);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    /*string fileName = roomType.RoomTypeID + Path.GetExtension(roomTypeDto.File.FileName);
                    //string filePath = @"wwwroot\RoomTypeImages\" + fileName;
                    string filePath = @"/OnlineHotelRoomBooking/Photos/" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    //roomType.Image = "/RoomTypeImages/" + fileName;
                    roomType.Image = fileName;*/


                    var baseFolder = _configuration.GetSection("BaseUrl:baseFolder").Value;
                    var imagesFolder = _configuration.GetSection("BaseUrl:imagesFolder").Value;

                    baseFolder = baseFolder + imagesFolder;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), baseFolder);
                    if (!Directory.Exists(filePath)) {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + "_" + roomType.File.FileName;

                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(fileStream);
                    }
                    roomType.Image = imagesFolder + fileName;
                }
                else
                {
                    roomType.Image = "http://placehold.co/600x400";
                }
                _db.RoomTypes.Add(roomType);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RoomTypeDto>(roomType);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update RoomType
        [HttpPut]
        [Route("{roomtypeId}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromRoute]int roomtypeId, [FromForm] RoomTypeDto roomTypeDto)
        {
            try
            {
                RoomType roomType = _mapper.Map<RoomType>(roomTypeDto);

                roomType.RoomTypeID = roomtypeId;
                
                if (roomTypeDto.File != null)
                {                        
                    var baseFolder = _configuration.GetSection("BaseUrl:baseFolder").Value;
                    var imagesFolder = _configuration.GetSection("BaseUrl:imagesFolder").Value;

                    baseFolder = baseFolder + imagesFolder;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), baseFolder);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + "_" + roomType.File.FileName;

                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(fileStream);
                    }
                    roomType.Image = imagesFolder + fileName;

                }


                _db.RoomTypes.Update(roomType);
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

        #region Delete RoomType
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                RoomType obj = _db.RoomTypes.First(u => u.RoomTypeID == id);
                if (!string.IsNullOrEmpty(obj.Image))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.Image);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
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
        #endregion
    }
}
