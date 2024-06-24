using AutoMapper;
using Azure;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using HotelBookingAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public UserAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        #region Get All User
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<ApplicationUser> objList = _db.ApplicationUsers.ToList();
                _response.Result = _mapper.Map<IEnumerable<ApplicationUserDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        /*#region Create ApplicationUsers
        [HttpPost]
        public ResponseDto Post([FromBody] ApplicationUserDto applicationUserDto)
        {
            try
            {
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(applicationUserDto);
                _db.ApplicationUsers.Add(applicationUser);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion*/

        #region GetUser By ID
        [HttpGet]
        [Route("User/"+"{userid}")]
        public ResponseDto GetByUser(string userid)
        {
            try
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.First(u => u.Id == userid);
                _response.Result = _mapper.Map<ApplicationUserDto>(applicationUser);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region GetByEmail
        [HttpGet]
        [Route("{email}")]
        public ResponseDto Get(string email)
        {
            try
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.First(u => u.Email == email);
                _response.Result = _mapper.Map<ApplicationUserDto>(applicationUser);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        /*#region Update User
        [HttpPut]
        public ResponseDto Put([FromBody] ApplicationUserDto applicationUserDto)
        {
            try
            {
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(applicationUserDto);
                
                _db.Users.Update(applicationUser);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ApplicationUserDto>(applicationUserDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion*/

        /*#region Update User
        [HttpPut]
        public ResponseDto Put([FromBody] UserDto userDto)
        {
            try
            {
                UserDto applicationUser = _mapper.Map<ApplicationUser>(userDto);

                _db.Users.Update(applicationUser);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ApplicationUserDto>(userDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion*/

        #region Delete User
        [HttpDelete]
        [Route("{userid}")]
        /*[Authorize(Roles = "ADMIN")]*/
        public ResponseDto Delete(string userid)
        {
            try
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.First(u => u.Id == userid);
                _db.Users.Remove(applicationUser);
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
