using MediatR;

namespace Products.API.Core.Features.Queries.GetProductById
{
    //  The class for the product detail query.
    public class GetProductByIdQuery: IRequest<GetProductByIdVm>
    {
        // Gets or sets the property for Id.
        public int Id { get; set; }
    }
}
