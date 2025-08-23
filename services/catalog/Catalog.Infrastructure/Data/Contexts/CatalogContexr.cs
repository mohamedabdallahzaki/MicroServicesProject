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
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

            var Db = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
             


            Types = Db.GetCollection<ProductType>(configuration["DatabaseSettings:TypesCollection"]);
            Brands = Db.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandsCollection"]);
            Products = Db.GetCollection<Product>(configuration["DatabaseSettings:ProductsCollection"]);

            _= ProductcontextSeed.SeedDataAsync(Products);
            _= BrandContextSeed.SeedDataAsync(Brands);
            _= TypeContextSeed.SeedDataAsync(Types);



        }

    }
}
