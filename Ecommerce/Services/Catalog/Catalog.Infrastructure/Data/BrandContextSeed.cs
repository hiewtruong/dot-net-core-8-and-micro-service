using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrand = brandCollection.Find(p => true).Any();
            if (!checkBrand)
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedPath = Path.Combine(basePath, "Data", "SeedData", "brands.json");
                var brandsData = File.ReadAllText(seedPath);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    brandCollection.InsertMany(brands);
                }
            }
        }
    }
}