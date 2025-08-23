using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string userName);

        Task<ShoppingCart> UpdateShoppingCart (ShoppingCart cart);

        Task   DeleteShoppingCart (string userName);
    }
}
