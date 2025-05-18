using ShoppingCart.Api.Models.Dto;

namespace ShoppingCart.Api.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
