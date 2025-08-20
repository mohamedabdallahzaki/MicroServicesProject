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
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IList<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository , IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository; 
        }
        public async Task<IList<ProductResponseDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProducts();

            var productListResponse = _mapper.Map<IList<ProductResponseDTO>>(productList.ToList());

            return productListResponse;
        }
    }
}
