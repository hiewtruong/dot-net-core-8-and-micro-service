using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProduct = productCollection.Find(p => true).Any();
            if (!checkProduct)
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedPath = Path.Combine(basePath, "Data", "SeedData", "products.json");
                var productsData = File.ReadAllText(seedPath);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        productCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}