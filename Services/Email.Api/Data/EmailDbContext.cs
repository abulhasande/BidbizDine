using Email.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Api.Data
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {

        }
        
        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
