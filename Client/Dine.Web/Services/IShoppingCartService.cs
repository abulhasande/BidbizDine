using Dine.Web.Models;

namespace Dine.Web.Services
{
    public interface IShoppingCartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> EmailCart(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsid);
        Task<ResponseDto?> ApplyCooponAsync(CartDto cartDto);

    }
}
