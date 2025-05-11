using Dine.Web.Models;

namespace Dine.Web.Services
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductByNameAsync(string productName);
        Task<ResponseDto?> GetAllProductAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(ProductDto productDto);


    }
}
