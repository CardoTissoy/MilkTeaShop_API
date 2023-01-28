using Products.API.Models;

namespace Products.API.Infrastructure.Data
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        // Initializes a new instance of the <see cref="ProductRepository"/> class.
        public ProductRepository(IAppDbContextProduct context)
           : base(context)
        {
        }
    }
}
