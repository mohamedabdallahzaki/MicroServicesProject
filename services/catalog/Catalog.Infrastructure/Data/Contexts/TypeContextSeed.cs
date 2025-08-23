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
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> collection)
        {
            var hasData = await collection.Find(_ => true).AnyAsync();

            if (hasData) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "type.json");
            using var type = File.OpenRead(path);


            var typeData = await JsonSerializer.DeserializeAsync<List<ProductType>>(type);

            if (typeData?.Any() is true)

                await collection.InsertManyAsync(typeData);

        }
    }
}
