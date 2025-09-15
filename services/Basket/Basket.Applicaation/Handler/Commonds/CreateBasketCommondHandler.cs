using AutoMapper;
using Basket.Applicaation.Commonds;
using Basket.Applicaation.GrpcServices;
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

        private readonly DiscountGrpcService _discountGrpcService;

        public CreateBasketCommondHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartRespones> Handle(CreateBasketCommond request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if(coupon is not null)
                {
                    item.Price -= coupon.Amount;
                }
            }

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
