using AutoMapper;
using Products.Api.Models;
using Products.Api.Models.Dtos;

namespace Products.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();

            });

            return mappingConfig;
        }
    }
}
