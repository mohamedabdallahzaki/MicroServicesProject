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
    public class GetAllProductsByBrandNameQueryHandler : IRequestHandler<GetAllProductsByBrandNameQuery, IList<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsByBrandNameQueryHandler(IProductRepository productRepository , IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDTO>> Handle(GetAllProductsByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var produtList = await _productRepository.GetAllProductsByName(request.BrandName);

            var productResponseList = _mapper.Map<IList<ProductResponseDTO>>(produtList.ToList());

            return productResponseList;
        }
    }
}
