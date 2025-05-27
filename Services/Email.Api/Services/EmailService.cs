using Email.Api.Data;
using Email.Api.Models;
using Email.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Email.Api.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<EmailDbContext> _dbContextOptions;

        public EmailService(DbContextOptions<EmailDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
           StringBuilder message = new StringBuilder();
            message.AppendLine("<br/> Cart Email Requested ");
            message.AppendLine("<br/> Total " + cartDto.CartHeader.CartTotal);
            message.AppendLine("<br/>");
            message.AppendLine("<ul>");
            foreach(var item in cartDto.CartDetails)
            {
                message.AppendLine("<li>");
                message.AppendLine(item.Product.Name + " * " + item.Count);
                message.AppendLine("</li>");
            }
            message.AppendLine("</ul>");

           await  LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
           string message = "User Registration Successful. <br/> Email: " + email;
            await LogAndEmail(message, "abul.hasan.de@gmail.com");

        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new EmailDbContext(_dbContextOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {

                return false;   
            }
        }
    }
}
