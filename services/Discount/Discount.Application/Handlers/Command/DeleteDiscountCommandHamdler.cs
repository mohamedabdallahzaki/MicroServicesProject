

using Discount.Application.Command;
using Discount.Core.Repositoires;
using MediatR;

namespace Discount.Application.Handlers.Command
{
    public class DeleteDiscountCommandHamdler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountCommandHamdler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
