using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }


        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSetteings:ConnectionString"]);

            var Db = client.GetDatabase(configuration["DatabaseSetteings:DatabaseName"]);

          Types   = Db.GetCollection<ProductType>("DatabaseSetteings:TypesCollection");
          Brands = Db.GetCollection<ProductBrand>("DatabaseSetteings:BrandsCollection");
          Products   = Db.GetCollection<Product>("DatabaseSetteings:ProductsCollection");

            _= ProductcontextSeed.SeedDataAsync(Products);
            _= BrandContextSeed.SeedDataAsync(Brands);
            _ = TypeContextSeed.SeedDataAsync(Types);

        }
    }
}
