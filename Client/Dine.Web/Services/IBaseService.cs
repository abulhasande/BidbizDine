using Dine.Web.Models;

namespace Dine.Web.Services
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
