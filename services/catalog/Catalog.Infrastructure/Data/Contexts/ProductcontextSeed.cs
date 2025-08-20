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

           using var productFile = File.OpenRead(@"..\Catalog.Infrastructure\Data\SeedData\product.json");

            var productData = await JsonSerializer.DeserializeAsync<List<Product>>(productFile);

            if (productData?.Any() is true)

                await collection.InsertManyAsync(productData);

        }
    }
}
