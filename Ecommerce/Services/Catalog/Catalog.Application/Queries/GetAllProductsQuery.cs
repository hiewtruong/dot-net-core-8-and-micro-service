using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
    {
    
    }
}

