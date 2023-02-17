using Newtonsoft.Json;

namespace MilkTea_WebApp.Entities
{
    public class ProductsDto
    {
        public List<ProductsInfo?> products { get; set; } = null!;
    }
    public class ProductsInfo
    {
        public int ProductId { get; set; }

        // Gets or sets the property for ProductCode.
        public string ProductCode { get; set; } = null!;

        // Gets or sets the property for ProductName.
        public string ProductName { get; set; } = null!;

        // Gets or sets the property for ProductDescription.
        public string ProductDescription { get; set; } = null!;

        // Gets or sets the property for Quantity.
        public int Quantity { get; set; }

        // Gets or sets the property for Price.
        public decimal Price { get; set; }
    }
}
