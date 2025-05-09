using Dine.Web.Models;

namespace Dine.Web.Services
{
    public interface ICooponService
    {
        Task<ResponseDto?> GetByCooponCodeAsync(string cooponCode);
        Task<ResponseDto?> GetAllCooponAsync();
        Task<ResponseDto?> GetCooponByIdAsync(int id);
        Task<ResponseDto?> UpdatCooponAsync(CooponDto cooponDto);
        Task<ResponseDto?> CreateCooponAsync(CooponDto cooponDto);
        Task<ResponseDto?> DeleteCooponAsync(CooponDto cooponDto);

    }
}
