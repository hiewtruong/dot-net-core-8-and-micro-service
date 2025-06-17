using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductByName(request.Name);
            var productResponseList = ProductMapper.Mapper.Map<IList<Product>,IList<ProductResponse>>(productList.ToList());
            return productResponseList;
        }
    }
}

