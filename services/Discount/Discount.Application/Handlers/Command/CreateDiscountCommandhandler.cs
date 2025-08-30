using AutoMapper;
using Discount.Application.Command;
using Discount.Application.Handlers.Queries;
using Discount.Core.Entities;
using Discount.Core.Repositoires;
using Discount.Grpc.Proto3;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Discount.Application.Handlers.Command
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommond, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository,  IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
         
        }

        public async Task<CouponModel> Handle(CreateDiscountCommond request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.CreateDsicount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }
    }
}
