using AutoMapper;
using Coupon.Api.Models;
using Coupon.Api.Models.Dtos;

namespace Coupon.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CooponDto, Coopon>();
                config.CreateMap<Coopon, CooponDto>();

            });

            return mappingConfig;
        }
    }
}
