using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.Core.Features.Commands.CreateProduct;
using Products.API.Core.Features.Queries.GetProduct;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.Json;
using Products.API.Core.Features.Queries.GetProductById;
using Products.API.Core.Features.Commands.DeleteProduct;
using Products.API.Core.Features.Commands.UpdateProduct;

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

        // This method will retrieve a product based on the product id parameter.
        [HttpGet("id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation("Get product by {id}", id);
            var product = await this.Mediatr.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                _logger.LogInformation("Get product by id {id} NotFound()", id);
                return NotFound();
            }
            _logger.LogInformation("Get product by id {id} ok().", id);
            return Ok(product);
        }

        //This method will create a product
        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var newProduct = JsonSerializer.Serialize(command);
            _logger.LogInformation("Create product :{newProduct}", newProduct);
            var productId = await this.Mediatr.Send(command);
            _logger.LogInformation("new productId: {productId}", productId);
            return this.Ok(productId);
        }

        // This method will delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete product by id {id}", id);
            var product = await Mediatr.Send(new DeleteProductCommand { Id = id });
            if (product == 1)
            {
                _logger.LogInformation("Delete product by id {id} Ok()", id);
                return Ok();
            }
            else 
            {
                _logger.LogInformation("Delete product by id {id} NotFound()", id);
                return NotFound();
            }
        }

        // This method will update a product
        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
        {
            var updateProduct = JsonSerializer.Serialize(command);
            _logger.LogInformation("Update product :{updateProduct}", updateProduct);
            if (id != command.ProductId)
            {
                _logger.LogInformation("Update product : command.productId = {ProductId} is not equal to id parameter = {id}.", command.ProductId, id);
                return this.BadRequest();
            }

            var product = await this.Mediatr.Send(command);

            if (product > 0)
            {
                _logger.LogInformation("Update product by product id {id} ok().", id);
                return this.Ok();
            }
            else
            {
                _logger.LogInformation("Update product by product id {id} NotFound().", id);
                return this.NotFound();
            }
        }
    }
}
