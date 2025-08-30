
using MediatR;

namespace Discount.Application.Command
{
    public class DeleteDiscountCommand:IRequest<bool>
    {
        public string ProductName { get; set; }

        public DeleteDiscountCommand(string productName)
        {
            ProductName = productName;
        }
    }
}
