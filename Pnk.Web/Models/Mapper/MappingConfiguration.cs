using AutoMapper;
using Pnk.Web.Models.Dto;
using Pnk.Web.Models.ViewModel;

namespace Pnk.Web.Models.Mapper
{
    public static class MappingConfiguration
    {
        public static MapperConfiguration GetMappingConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductViewModel, ProductDto>().ReverseMap();
            });
    
        }
    }
}
