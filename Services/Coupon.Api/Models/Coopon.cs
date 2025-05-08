using System.ComponentModel.DataAnnotations;

namespace Coupon.Api.Models
{
    public class Coopon
    {
        [Key]
        public int CouponId { get; set; }

        [Required]
        public string CouponCode { get; set; }

        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount {  get; set; }
    }
}
