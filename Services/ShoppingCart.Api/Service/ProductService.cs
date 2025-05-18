
using Newtonsoft.Json;
using ShoppingCart.Api.Models.Dto;

namespace ShoppingCart.Api.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var responseDeserialObj = JsonConvert.DeserializeObject<ResponseDto>(apiContent); 
            if(responseDeserialObj.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDeserialObj.Result));
            }


            return new List<ProductDto>();
        }
    }
}
