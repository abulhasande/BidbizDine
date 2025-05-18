using AutoMapper;
using Coupon.Api.Data;
using Coupon.Api.Models;
using Coupon.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Api.Controllers
{
    [Route("api/coopon")]
    [ApiController]
    //[Authorize]
    public class CooponController : ControllerBase
    {
        private readonly CooponDbContext dbContext;
        private ResponseDto responseDto;
        private IMapper mapper; 

        public CooponController(CooponDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            responseDto= new ResponseDto();
            this.mapper = mapper;   
        }

        [HttpGet]
        public ResponseDto GetCoopon()
        {
            try
            {
                IEnumerable<Coopon> coopons  = dbContext.Coopons.ToList();
                responseDto.Result = mapper.Map<IEnumerable<CooponDto>>(coopons);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;

         }

        [HttpGet("{id:int}")]
        public ResponseDto GetCooponById(int id)
        {

            try
            {
                Coopon coopon = dbContext.Coopons.First(x => x.CouponId == id);
                responseDto.Result = mapper.Map<CooponDto>(coopon);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;

        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDto GetCooponByCode(string code)
        {

            try
            {
                Coopon coopon = dbContext.Coopons.First(x => x.CouponCode.ToLower() == code.ToLower());
                responseDto.Result = mapper.Map<CooponDto>(coopon);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;

        }


        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto CreateCoopon([FromBody] CooponDto cooponDto)
        {

            try
            {
                Coopon coopon = mapper.Map<Coopon>(cooponDto);
                dbContext.Add(coopon);
                dbContext.SaveChanges();

                responseDto.Result = mapper.Map<CooponDto>(coopon);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;

        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateCoopon([FromBody] CooponDto cooponDto)
        {

            try
            {
                Coopon coopon = mapper.Map<Coopon>(cooponDto);
                dbContext.Update(coopon);
                dbContext.SaveChanges();

                responseDto.Result = mapper.Map<CooponDto>(coopon);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto DeleteCoopon(int  id)
        {

            try
            {
                Coopon coopon = dbContext.Coopons.First(x => x.CouponId == id);
                dbContext.Remove(coopon);
                dbContext.SaveChanges();

               }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;

        }


    }
}
