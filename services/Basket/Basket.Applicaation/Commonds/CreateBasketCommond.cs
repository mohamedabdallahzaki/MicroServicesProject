using Basket.Applicaation.Responses;
using Basket.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Commonds
{
    public class CreateBasketCommond:IRequest<ShoppingCartRespones>
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; }


        public CreateBasketCommond(string userName , List<ShoppingCartItem> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
