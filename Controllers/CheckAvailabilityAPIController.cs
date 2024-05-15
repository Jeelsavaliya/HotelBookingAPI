using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using HotelBookingAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckAvailabilityAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;


        public CheckAvailabilityAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        #region Create CheckAvailabilitys
        [HttpPost]
        public ResponseDto Post([FromForm] CheckAvailabilityDto checkAvailabilityDto)
        {
            try
            {
                CheckAvailability checkAvailability = _mapper.Map<CheckAvailability>(checkAvailabilityDto);
                _db.CheckAvailabilitys.Add(checkAvailability);
                _db.SaveChanges();

               
                _response.Result = _mapper.Map<CheckAvailabilityDto>(checkAvailability);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete CheckAvailability
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                CheckAvailability obj = _db.CheckAvailabilitys.First(u => u.CheckAvailabilityID == id);
                _db.CheckAvailabilitys.Remove(obj);
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
