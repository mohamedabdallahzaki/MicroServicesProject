using AutoMapper;
using Catalog.Application.Query;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler.Query
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Pagantion<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository , IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository; 
        }
        public async Task<Pagantion<ProductResponseDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProducts(request.Spec);

            var productListResponse = _mapper.Map<Pagantion<ProductResponseDTO>>(productList);

            return productListResponse;
        }
    }
}
