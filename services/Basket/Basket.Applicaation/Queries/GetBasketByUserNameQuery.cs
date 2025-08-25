﻿using Basket.Applicaation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Applicaation.Queries
{
    public class GetBasketByUserNameQuery:IRequest<ShoppingCartRespones>
    {
        public string UserName {  get; set; }


        public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName;
            
        }
    }
}
