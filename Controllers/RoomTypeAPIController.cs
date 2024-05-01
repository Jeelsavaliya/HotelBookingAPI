﻿using AutoMapper;
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

        #region Get All RoomType 
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
        #endregion

        #region Get Roomtypw
        [HttpGet("{id}")]
        public ResponseDto Get([FromRoute]int id)
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
                _db.RoomTypes.Add(roomType);
                _db.SaveChanges();

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

                    string fileName = roomType.RoomTypeID + Path.GetExtension(roomTypeDto.File.FileName);
                    string filePath = @"wwwroot\RoomTypeImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    roomType.Image = "/RoomTypeImages/" + fileName;
                   /* roomType.Image = filePath;*/
                }
                else
                {
                    roomType.Image = "https://placehold.co/600x400";
                }
                _db.RoomTypes.Update(roomType);
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
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromForm] RoomTypeDto roomTypeDto)
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

                    string fileName = roomType.RoomTypeID + Path.GetExtension(roomTypeDto.File.FileName);
                    string filePath = @"wwwroot/RoomTypeImages/" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        roomTypeDto.File.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    roomType.Image = "/RoomTypeImages/" + fileName;
                    /*roomType.Image = filePath;*/
                }

                _db.RoomTypes.Update(roomType);
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
