using Azure;
using Microsoft.Extensions.Options;
using MilkTea_WebApp.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MilkTea_WebApp.Services
{
    public class ProductServiceRepository: IProductServiceRepository
    {
        private readonly HttpClient _httpClient;
        public ProductServiceRepository(HttpClient httpClient)
        {
            _httpClient= httpClient;
        }


        public List<ProductsDto> Products { get; set; } = new List<ProductsDto>();


        public async Task GetProducts()
        {
            var response = await _httpClient.GetStringAsync("api/Product");

            if (response != null)
            {
                Products = JsonConvert.DeserializeObject<ProductsDto>(response);
            }
            
        }
    }
}
