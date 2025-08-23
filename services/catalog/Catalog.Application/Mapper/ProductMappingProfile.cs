using AutoMapper;
using Catalog.Application.Commond;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mapper
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, ProductBrandDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<ProductType, TypeResponseDTO>().ReverseMap();
            CreateMap<CreateProductCommond, Product>().ReverseMap();
   
            CreateMap<Pagantion<Product>, Pagantion<ProductResponseDTO>>().ReverseMap();

        }
    }
}
