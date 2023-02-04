using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.Core.Features.Commands.CreateProduct;
using Products.API.Core.Features.Queries.GetProduct;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.Json;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ILogger _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger= logger;
        }

        // This method will retrieve all the products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Get all information");
            var products = await this.Mediatr.Send(new GetProductListQuery());
            if (products == null)
            {
                _logger.LogInformation("Get all products NotFound()");
                return NotFound();
            }

            _logger.LogInformation("Get all the products ok()");
            return Ok(products);
        }

        //This method will create a product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var newProduct = JsonSerializer.Serialize(command);
            _logger.LogInformation("Create product :{newProduct}", newProduct);
            var productId = await this.Mediatr.Send(command);
            _logger.LogInformation("new productId: {productId}", productId);
            return this.Ok(productId);
        }
    }
}
