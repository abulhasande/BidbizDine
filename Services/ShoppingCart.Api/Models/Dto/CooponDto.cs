namespace ShoppingCart.Api.Models.Dto
{
    public class CooponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = default!; 
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
