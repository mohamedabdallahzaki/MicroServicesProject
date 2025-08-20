using AutoMapper;
using Catalog.Application.Commond;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler.Commond
{
    public class CreateProductCommondHandler : IRequestHandler<CreateProductCommond, ProductResponseDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommondHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductResponseDTO> Handle(CreateProductCommond request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);

            var product = await _productRepository.CreateProduct(productEntity);

            var productResponse = _mapper.Map<ProductResponseDTO>(product);

            return productResponse;
        }
    }
}
