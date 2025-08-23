using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _Cache;

        public BasketRepository(IDistributedCache cache)
        {
            _Cache = cache;
        }
        public async Task<ShoppingCart> GetShoppingCart(string userName)
        {
            var basket = await _Cache.GetStringAsync(userName);

            if (basket == null) return null;

            return   JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task DeleteShoppingCart(string userName)
        {
           var basket = await GetShoppingCart(userName);
            if(basket is not null)
            {

                await _Cache.RemoveAsync(userName);
            }
        }


        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart)
        {

            var basket = await GetShoppingCart(cart.UserName);
            if(basket is not null)
            {
                return basket;
            }

            await _Cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await GetShoppingCart(cart.UserName);

        }
    }
}
