using AutoMapper;
using eCommerce.DataAccess.Dtos;
using eCommerce.DataAccess.Entities;

namespace eCommerce.API.Utilities.Mapper.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
