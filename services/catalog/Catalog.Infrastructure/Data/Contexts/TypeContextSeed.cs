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

           using var type = File.OpenRead(@"..\Catalog.Infrastructure\Data\SeedData\type.json");

            var typeData = await JsonSerializer.DeserializeAsync<List<ProductType>>(type);

            if (typeData?.Any() is true)

                await collection.InsertManyAsync(typeData);

        }
    }
}
