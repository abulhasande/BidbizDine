using Email.Api.Models.Dto;

namespace Email.Api.Services
{
    public interface IEmailService
    {
        Task  EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
    }
}
