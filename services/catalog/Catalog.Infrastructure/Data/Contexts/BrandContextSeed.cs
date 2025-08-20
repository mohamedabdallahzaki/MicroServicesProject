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
    public static  class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> collection)
        {
            var hasData = await collection.Find(_ => true).AnyAsync();

            if (hasData) return;

           using  var brand = File.OpenRead(@"..\Catalog.Infrastructure\Data\SeedData\brand.json");

            var brandData = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brand);

            if(brandData?.Any() is true)

                    await collection.InsertManyAsync(brandData);

        }

    }
}
