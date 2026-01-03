using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByBrandNameQuery  : IRequest<IList<ProductResponse>>
    {
        public string Name { get; set; }
        public GetProductByBrandNameQuery(string name)
        {
            Name = name;
        }
    }
}

