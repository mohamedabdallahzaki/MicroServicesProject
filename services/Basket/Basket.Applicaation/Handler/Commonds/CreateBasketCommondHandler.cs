using AutoMapper;
using Basket.Applicaation.Commonds;
using Basket.Applicaation.Responses;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Handler.Commonds
{
    public class CreateBasketCommondHandler : IRequestHandler<CreateBasketCommond, ShoppingCartRespones>
    {
        private readonly IBasketRepository _basketRepository;

        private readonly IMapper _mapper;

        public CreateBasketCommondHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<ShoppingCartRespones> Handle(CreateBasketCommond request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.UpdateShoppingCart(new Core.Entities.ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });

            var basketRespons = _mapper.Map<ShoppingCartRespones>(basket);

            return basketRespons;
        }
    }
}
