using Coupon.Api.Models;

namespace Coupon.Api.Data
{
    public class InitialData
    {

     public static IEnumerable<Coopon> Coopons =>
        new List<Coopon>
        {
             new Coopon()
             {  
                CouponId = 1,
                CouponCode= "JB007",
                DiscountAmount=10,
                MinAmount = 120
             },
             new Coopon()
             {
                CouponId = 2,
                CouponCode= "JB008",
                DiscountAmount=5,
                MinAmount = 120
             },
             new Coopon()
             {
                CouponId = 3,
                CouponCode= "JB009",
                DiscountAmount=50,
                MinAmount = 620
             }

        };
    }
}
