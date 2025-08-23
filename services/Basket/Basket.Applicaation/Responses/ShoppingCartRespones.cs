using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Responses
{
    public class ShoppingCartRespones
    {
        public string UserName { get; set; }

        public List<ShoppingItemResponse> Items { get; set; } = new List<ShoppingItemResponse>();


        public ShoppingCartRespones()
        {
            
        }


        public ShoppingCartRespones(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {

            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
