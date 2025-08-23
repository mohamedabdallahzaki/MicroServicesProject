using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class ProductcontextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> collection)
        {
            var hasData = await collection.Find(_ => true).AnyAsync();

            if (hasData) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "product.json");
            using var product = File.OpenRead(path);

            var productData = await JsonSerializer.DeserializeAsync<List<Product>>(product);

            if (productData?.Any() is true)

                await collection.InsertManyAsync(productData);

        }
    }
}
