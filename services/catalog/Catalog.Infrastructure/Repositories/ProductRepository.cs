using Catalog.Core.Entities;
using Catalog.Core.Repositories;
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

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();

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
    }
}
