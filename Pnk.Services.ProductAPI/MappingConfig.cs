using AutoMapper;
using Pnk.Services.ProductAPI.Models;
using Pnk.Services.ProductAPI.Models.Dto;

namespace Pnk.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // map as long as their names are same.
                config.CreateMap<ProductDto, Product>().ReverseMap(); // map product-productdto and productdto-product 
      

            });

            return mappingConfig;
        }
            
    }
}
