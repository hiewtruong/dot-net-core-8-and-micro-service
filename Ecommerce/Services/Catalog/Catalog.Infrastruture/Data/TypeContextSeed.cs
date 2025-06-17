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
            string path = Path.Combine("Data", "SeedData", "types.json");
            if (!checkType)
            {
                var typesData = File.ReadAllText(path);
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