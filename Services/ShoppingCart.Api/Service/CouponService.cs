using Newtonsoft.Json;
using ShoppingCart.Api.Models.Dto;

namespace ShoppingCart.Api.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CooponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coopon");
            var response = await client.GetAsync($"/api/coopon/GetByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var responseDeserialObj = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (responseDeserialObj.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CooponDto> (Convert.ToString(responseDeserialObj.Result));
            }


            return new CooponDto();
        }
    }
}
