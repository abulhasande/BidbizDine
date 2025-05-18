using AutoMapper;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Models.Dto;

namespace ShoppingCart.Api
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
