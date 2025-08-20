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
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<ProductBrandDTO>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(IBrandRepository brandRepository , IMapper mapper)
        {
            _brandRepository = brandRepository;   
            _mapper = mapper;
        
        }

        public async Task<IList<ProductBrandDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {

            var brands = await _brandRepository.GetAllBrand();

            var brandsResponse = _mapper.Map<IList<ProductBrandDTO>>(brands.ToList());

            return brandsResponse;
        }
    }
}
