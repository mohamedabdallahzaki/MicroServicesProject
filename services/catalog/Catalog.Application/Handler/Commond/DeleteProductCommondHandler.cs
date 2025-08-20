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
    public class DeleteProductCommondHandler : IRequestHandler<DeleteProductCommond, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommondHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommond request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProduct(request.Id);
        }
    }
}
