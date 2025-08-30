using AutoMapper;
using Discount.Application.Command;
using Discount.Core.Entities;
using Discount.Core.Repositoires;
using Discount.Grpc.Proto3;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers.Command
{
    public class UpdataDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;


        public UpdataDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;

        }

        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.UpdateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }
    }
}
