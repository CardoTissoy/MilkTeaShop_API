using Microsoft.AspNetCore.Components;
using MilkTea_WebApp.Entities;
using MilkTea_WebApp.Services;

namespace MilkTea_WebApp.Pages
{
    public class ProductsOverviewClass: ComponentBase
    {
        public ProductsDto Products { get; set; } = new ProductsDto();
        [Inject]
        public IProductServiceRepository _productServiceRepository { get; set; } = null!;

        protected async override Task OnInitializedAsync() =>  await _productServiceRepository.GetProducts();
        
    }
}
