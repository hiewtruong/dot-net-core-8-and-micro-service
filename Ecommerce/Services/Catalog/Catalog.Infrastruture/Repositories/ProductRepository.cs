using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastruture.Data;
using MongoDB.Driver;

namespace Catalog.Infrastruture.Repositories
{
    public class ProductRepository :IProductRepository, IBrandRepository, ITypesRepository
    {
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        
        public ICatalogContext _context { get; }
        
        public async Task<IEnumerable<Product>> GetProducts()
        {
           return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context
                .Products
                .Find(p => p.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string brand)
        {
            return await _context
                .Products
                .Find(p => p.Brands.Name.ToLower() == brand.ToLower())
                .ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);
            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _context
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteProduct = await _context
                .Products
                .DeleteOneAsync(p => p.Id == id);
            return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrand()
        {
            return await _context
                .Brands
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                .Types
                .Find(p => true)
                .ToListAsync();
        }
    }
}

