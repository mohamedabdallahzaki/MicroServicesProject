using Discount.Grpc.Proto3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            Console.WriteLine($"Calling Discount gRPC Service for product: {productName}");

            var discountRequest = new GetDiscountRequest { ProductName = productName};
            var result = await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
            Console.WriteLine($"Received coupon: {result?.Amount}");
            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}    

