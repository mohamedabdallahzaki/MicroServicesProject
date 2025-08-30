using Discount.Application.Command;
using Discount.Application.Queries;
using Discount.Grpc.Proto3;
using Grpc.Core;
using MediatR;

namespace Discount.Api.services
{
    public class DiscountServices:DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);

            var res = await _mediator.Send(query);
            return res;
        }

        public override async Task<DeleteDiscountRespones> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cmd = new DeleteDiscountCommand(request.ProductName);

            var deleted = await _mediator.Send(cmd);

            return new DeleteDiscountRespones
            {
                Success = deleted,
            };
        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cmd = new CreateDiscountCommond()
            {
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                ProductName = request.Coupon.ProductName
            };

            var res = await _mediator.Send(cmd);

            return res;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cmd = new UpdateDiscountCommand()
            {
                Id = request.Coupon.Id,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                ProductName = request.Coupon.ProductName
            };

            var res = await _mediator.Send(cmd);

            return res; 
        }
    }
}
