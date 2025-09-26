using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entites;


namespace Basket.Application.Mappers
{
    public class BasketMappingProfile :Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart,ShoppingCartResponse>().ReverseMap();
            CreateMap<ShoppingCartItem,ShoppingCartItemResponse>().ReverseMap();
            
        }
    }
}
