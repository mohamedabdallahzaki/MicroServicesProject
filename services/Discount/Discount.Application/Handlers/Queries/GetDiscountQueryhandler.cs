using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositoires;
using Discount.Grpc.Proto3;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers.Queries
{
    public class GetDiscountQueryhandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDiscountQueryhandler> _logger;

        public GetDiscountQueryhandler(IDiscountRepository discountRepository, ILogger<GetDiscountQueryhandler> logger , IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"This product {request.ProductName} no has discouunt"));
            }

            var couponResponse = _mapper.Map<CouponModel>(coupon);
            _logger.LogInformation($"The coupon of product {request.ProductName} is fetched ") ;
            return couponResponse;
        }
    }
}
