using MilkTea_WebApp.Entities;

namespace MilkTea_WebApp.Services
{
    public interface IProductServiceRepository
    {
        List<ProductsDto> Products { get; set; }
        Task  GetProducts();
    }
}
