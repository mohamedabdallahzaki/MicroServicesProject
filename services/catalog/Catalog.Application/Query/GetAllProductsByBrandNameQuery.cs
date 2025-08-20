using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Query
{
    public class GetAllProductsByBrandNameQuery:IRequest<IList<ProductResponseDTO>>
    {
        public string BrandName { get; set; }

        public GetAllProductsByBrandNameQuery(string brandname)
        {
            BrandName = brandname;
        }
    }
}
