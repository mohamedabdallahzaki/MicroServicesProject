
using AutoMapper;
using Discount.Application.Command;
using Discount.Core.Entities;
using Discount.Grpc.Proto3;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Discount.Application.Mapper
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<CreateDiscountCommond, Coupon>();
            CreateMap<UpdateDiscountCommand, Coupon>();
        }
    }
}
