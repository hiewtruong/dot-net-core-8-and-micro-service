using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastruture.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrand = brandCollection.Find(p => true).Any();
            string path = Path.Combine("Data", "SeedData","brands.json");
            if (!checkBrand)
            {
                var brandsData = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}