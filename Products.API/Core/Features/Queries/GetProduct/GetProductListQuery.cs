using MediatR;

namespace Products.API.Core.Features.Queries.GetProduct
{
    public class GetProductListQuery: IRequest<ProductListVm>
    {
    }
}
