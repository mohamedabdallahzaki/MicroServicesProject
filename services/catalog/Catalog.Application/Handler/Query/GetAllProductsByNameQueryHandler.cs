using AutoMapper;
using Catalog.Application.Query;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler.Query
{
    public class GetAllProductsByNameQueryHandler : IRequestHandler<GetAllProductsByNameQuery, IList<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDTO>> Handle(GetAllProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductsByName(request.Name);
            var productResponseList = _mapper.Map<IList<ProductResponseDTO>>(productList);
            return productResponseList;
        }
    }
}
