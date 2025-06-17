using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllTypesQuery : IRequest<IList<TypeResponse>>
    {
    
    }
}

