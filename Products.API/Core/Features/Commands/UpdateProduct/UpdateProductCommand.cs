using MediatR;
using Products.API.Core.DTOs;

namespace Products.API.Core.Features.Commands.UpdateProduct
{
    public class UpdateProductCommand: ProductDto, IRequest<long>
    {
    }
}
