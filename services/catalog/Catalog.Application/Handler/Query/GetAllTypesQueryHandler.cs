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
    public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponseDTO>>
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public GetAllTypesQueryHandler(ITypeRepository typeRepository , IMapper mapper)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
        }
        public async Task<IList<TypeResponseDTO>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typeRepository.GetAllTypes();

            var typeResponseList = _mapper.Map<IList<TypeResponseDTO>>(typeList.ToList());

            return typeResponseList;
        }
    }
}
