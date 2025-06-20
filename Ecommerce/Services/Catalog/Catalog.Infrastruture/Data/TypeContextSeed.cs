using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastruture.Data
{
    public class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkType = typeCollection.Find(p => true).Any();
            if (!checkType)
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedPath = Path.Combine(basePath, "Data", "SeedData", "types.json");
                var typesData = File.ReadAllText(seedPath);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var item in types)
                    {
                        typeCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}