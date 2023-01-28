using MediatR;
using Products.API.Core.DTOs;

namespace Products.API.Core.Features.Commands.CreateProduct
{
    public class CreateProductCommand: ProductDto, IRequest<long>
    {
    }
}
