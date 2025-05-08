using Coupon.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon.Api.Data
{
    public class CooponDbContext : DbContext
    {
        public CooponDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coopon> Coopons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Coopon>().HasData(new Coopon
            {
                CouponId = 1,
                CouponCode= "JB007",
                DiscountAmount=10,
                MinAmount = 120
            });

            modelBuilder.Entity<Coopon>().HasData(new Coopon
            {
                CouponId = 2,
                CouponCode = "JB009",
                DiscountAmount = 20,
                MinAmount = 180
            });


        }
    }
}
