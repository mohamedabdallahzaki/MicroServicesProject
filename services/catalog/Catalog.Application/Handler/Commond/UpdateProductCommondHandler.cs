using AutoMapper;
using Catalog.Application.Commond;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler.Commond
{
    public class UpdateProductCommondHandler : IRequestHandler<UpdateProductCommond, bool>
    {
        private readonly IProductRepository _productRepository;
  

        public UpdateProductCommondHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommond request, CancellationToken cancellationToken)
        {
            var res = await _productRepository.UpdateProduct(new Core.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                Brand = request.Brand,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Summary = request.Summary,
                Price = request.Price,
                Type = request.Type
            });

            return res;
        }
    }
}
