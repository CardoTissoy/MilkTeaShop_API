using Products.API.Core.DTOs;

namespace Products.API.Core.Features.Queries.GetProduct
{
    // Product list view model
    public class ProductListVm
    {
        public IList<ProductDto>? Products { get; set; }
    }
}
