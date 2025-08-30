﻿using Discount.Grpc.Proto3;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Command
{
    public class CreateDiscountCommond :IRequest<CouponModel>
    {
        public string ProductName {  get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }
    }
}
