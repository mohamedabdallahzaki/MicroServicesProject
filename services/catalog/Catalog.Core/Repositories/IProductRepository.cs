using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(string id);

        Task<IEnumerable<Product>> GetAllProductsByName(string name);

        Task<IEnumerable<Product>> GetAllProductsByBrandName(string name);

        Task<Product> CreateProduct(Product product);


        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string id);




    }
}
