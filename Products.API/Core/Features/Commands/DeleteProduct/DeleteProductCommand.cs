using MediatR;

namespace Products.API.Core.Features.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<long>
    {
        // Get or sets the property for id
        public int Id { get; set; }
    }
}
