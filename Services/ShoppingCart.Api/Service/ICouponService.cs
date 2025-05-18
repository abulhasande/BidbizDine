using ShoppingCart.Api.Models.Dto;

namespace ShoppingCart.Api.Service
{
    public interface ICouponService
    {
        Task<CooponDto> GetCoupon(string coupon); 
    }
}
