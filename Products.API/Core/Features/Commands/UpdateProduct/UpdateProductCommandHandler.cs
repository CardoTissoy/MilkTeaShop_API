using Azure.Core;
using MediatR;
using Products.API.Infrastructure.Data;
using System.Text.Json;

namespace Products.API.Core.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler: IRequestHandler <UpdateProductCommand, long>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IConfiguration configuration, ILogger<UpdateProductCommandHandler> logger, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Update product id {productId} : {timestamp}.", command.ProductId, DateTime.Now);
                if (_configuration.GetValue<bool>("EnableDetailedLog"))
                {
                    var updateProduct = JsonSerializer.Serialize(command);
                    _logger.LogInformation("Update product = {updateProduct}", updateProduct);
                }

                var entity = _repository.GetById(command.ProductId);
                if (entity == null)
                {
                    _logger.LogInformation("Update product id: {productId} not found", command.ProductId);
                    return 0;
                }

                entity.ProductName = command.ProductName;
                entity.ProductDescription = command.ProductDescription;
                entity.ProductCode = command.ProductCode;
                entity.Price = command.Price;
                entity.Quantity = command.Quantity;
                _repository.Update(entity);
                _unitOfWork.Save();

                _logger.LogInformation("Upddate product id is ok()");
                return entity.ProductId;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Update product handler {ex}", ex);
                return -1;
            }
          



        }
    }
}
