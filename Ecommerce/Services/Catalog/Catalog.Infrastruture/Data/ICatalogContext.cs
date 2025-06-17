using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastruture.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ProductType> Types { get; }
        IMongoCollection<ProductBrand> Brands { get; }
    }
}

