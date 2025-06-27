using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Response;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<CreateProductCommand, Product>(request);
            if (productEntity == null)
            {
                throw new ApplicationException("Product is null");
            }
            var product = await _productRepository.CreateProduct(productEntity);
            var productResponse = ProductMapper.Mapper.Map<Product, ProductResponse>(product);
            return productResponse;
        }
    }
}

