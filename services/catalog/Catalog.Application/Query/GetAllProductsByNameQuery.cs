using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Query
{
    public class GetAllProductsByNameQuery:IRequest<IList<ProductResponseDTO>>
    {
        public string Name { get; set; }


        public GetAllProductsByNameQuery(string name)
        {
            Name = name;
        }
    }
}
