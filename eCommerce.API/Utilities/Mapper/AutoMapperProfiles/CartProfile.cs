using AutoMapper;
using eCommerce.DataAccess.Dtos;
using eCommerce.DataAccess.Entities;

namespace eCommerce.API.Utilities.Mapper.AutoMapperProfiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartDetailAddDto, CartDetail>();
            CreateMap<CartDetailUpdateDto, CartDetail>();
        }
    }
}
