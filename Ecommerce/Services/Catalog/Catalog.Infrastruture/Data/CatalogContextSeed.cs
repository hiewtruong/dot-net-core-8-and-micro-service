using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastruture.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProduct = productCollection.Find(p => true).Any();
            string path = Path.Combine("Data", "SeedData","products.json");
            if (!checkProduct)
            {
                var productsData = File.ReadAllText(path);
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