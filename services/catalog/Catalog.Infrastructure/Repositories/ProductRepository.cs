using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagantion<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if(!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }

            if(!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typeFilter = builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
                filter &= typeFilter;
            }
            var itemToatl = await _catalogContext.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParams, filter);
            return new Pagantion<Product>()
            {
                PageIndex = catalogSpecParams.PageIndex,
                PageSize = catalogSpecParams.PageSize,
                Count = (int)itemToatl,
                Data = data
            };

        }
        public async Task<Product> CreateProduct(Product product)
        {
            
            await _catalogContext.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<bool> DeleteProduct(string  id)
        {
            var res = await _catalogContext.Products.DeleteOneAsync(p => p.Id == id);

            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrand()
        {
            return await _catalogContext.Brands.Find(p => true).ToListAsync();
        }

      

        public async Task<IEnumerable<Product>> GetAllProductsByBrandName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Brand.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _catalogContext.Types.Find(p => true).ToListAsync();
        }

       

        public async Task<bool> UpdateProduct(Product product)
        {
            var res = await _catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id,product);

            return res.IsAcknowledged&&res.ModifiedCount > 0;
        }

        private async Task<IReadOnlyList<Product>> DataFilter (CatalogSpecParams catalogSpecParams , FilterDefinition<Product> filter)
        {
            var sortDef = Builders<Product>.Sort.Ascending("Name");
            if(!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch(catalogSpecParams.Sort)
                {
                    case "PriceAsc":
                        sortDef = Builders<Product>.Sort.Ascending(p => p.Price);
                            break;
                    case "PriceDesc":
                        sortDef = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDef = Builders<Product>.Sort.Ascending("Name");
                        break;


                }
                    
            }

            return await
                _catalogContext.Products.Find(filter).Sort(sortDef)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1)).Limit(catalogSpecParams.PageSize).ToListAsync();

        }

    
    }
}
