using Basket.Applicaation.Commonds;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Handler.Commonds
{
    public class DeleteBasketCommondHandler : IRequestHandler<DeleteBasketCommond, Unit>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommondHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Unit> Handle(DeleteBasketCommond request, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteShoppingCart(request.UserName);

            return Unit.Value;

        }
    }
}
