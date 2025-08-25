using AutoMapper;
using Basket.Applicaation.Queries;
using Basket.Applicaation.Responses;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Applicaation.Handler.Queries
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartRespones>
    {
        private readonly IBasketRepository _basketRepository;

        private readonly IMapper _mapper;

        public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<ShoppingCartRespones> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetShoppingCart(request.UserName);

            var basketResponse = _mapper.Map<ShoppingCartRespones>(basket);

            return basketResponse;
        }
    }
}
