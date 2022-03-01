using AutoMapper;
using eCommerce.DataAccess.Dtos;
using eCommerce.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.API.Utilities.Extentions
{
    public static class MapperExtesions
    {
        private static IMapper _mapper;

        private static IMapper mapper
        {
            get

            {
                if (_mapper is null)
                    _mapper = StaticServiceProvider.Provider.GetService<IMapper>();

                return _mapper;
            }
        }

        public static Product ToProduct(this ProductAddDto productAddDto)
        {
            return mapper.Map<Product>(productAddDto);
        }
        public static Product ToProduct(this ProductUpdateDto productUpdateDto)
        {
            return mapper.Map<Product>(productUpdateDto);
        }
        public static CartDetail ToCartDetail(this CartDetailAddDto cartDetailAddDto)
        {
            return mapper.Map<CartDetail>(cartDetailAddDto);
        }
        public static CartDetail ToCartDetail(this CartDetailUpdateDto cartDetailUpdateDto)
        {
            return mapper.Map<CartDetail>(cartDetailUpdateDto);
        }
    }
}
