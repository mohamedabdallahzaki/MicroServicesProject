using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Commonds
{
    public class DeleteBasketCommond:IRequest<Unit>
    {
        public string UserName { get; set; }

        public DeleteBasketCommond(string userName)
        {
            UserName = userName;
        }
    }
}
