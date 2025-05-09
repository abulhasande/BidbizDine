using Dine.Web.Models;
using Dine.Web.Utility;
using static Dine.Web.Utility.SD;

namespace Dine.Web.Services
{
    public class CooponService : ICooponService
    {
        private readonly IBaseService _baseService;
        public CooponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetAllCooponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType =  ApiType.GET, 
                Url= SD.CooponAPIBase + "/api/coopon"
            });
        }

        public async  Task<ResponseDto?> GetByCooponCodeAsync(string cooponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.CooponAPIBase + "/api/coopon/GetByCode/" + cooponCode
            });
        }

        public async Task<ResponseDto?> GetCooponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.CooponAPIBase + "/api/coopon/" + id
            });
        }


        public async Task<ResponseDto?> CreateCooponAsync(CooponDto cooponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cooponDto,
                Url = SD.CooponAPIBase + "/api/coopon"
            });
        }

        public async Task<ResponseDto?> DeleteCooponAsync(CooponDto cooponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.CooponAPIBase + "/api/coopon/" + cooponDto.CouponId
            });
        }



        public async Task<ResponseDto?> UpdatCooponAsync(CooponDto cooponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = cooponDto,
                Url = SD.CooponAPIBase + "/api/coopon"
            });
        }
    }
}
