
using MediatR;

namespace Ordering.Application.Commands
{
    public class checkoutOrderCommondV2 :IRequest<int>
    {
        public string UserName { get; set; }

        public decimal TotalPrice { get; set;}
    }
}
