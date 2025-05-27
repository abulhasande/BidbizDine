using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Email.Api.Models
{
    public class EmailLogger
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
