using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Products.API.Core.Features.Commands.CreateProduct
{
    public class CreateProductCommandhandler: IRequestHandler<CreateProductCommand, long>
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public CreateProductCommandhandler(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        // The Method will create Product
        public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken) 
        {
            try
            {
                _logger.LogInformation("Create a product {timestamp}", DateTime.Now);
                if (_configuration.GetValue<bool>("EnableDetailedLog"))
                {
                    var newProduct = JsonSerializer.Serialize(command);
                    _logger.LogInformation("create a product {newProduct}", newProduct);
                }

                /// Pending: to be continue on mapping product properties.
            }
            catch (Exception ex)
            {
            }
        }
    }
}
