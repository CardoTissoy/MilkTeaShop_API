using System.ComponentModel.DataAnnotations.Schema;

namespace Products.API.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int VendorId { get; set; } 
    }
}
