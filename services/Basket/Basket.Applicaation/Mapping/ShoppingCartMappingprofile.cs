using AutoMapper;
using Basket.Applicaation.Responses;
using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Mapping
{
    public class ShoppingCartMappingprofile:Profile
    {
        public ShoppingCartMappingprofile()
        {
            CreateMap<ShoppingCart, ShoppingCartRespones>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingItemResponse>().ReverseMap();
        }
    }
}
