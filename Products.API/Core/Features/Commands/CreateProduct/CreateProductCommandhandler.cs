using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.API.Core.DTOs;
using Products.API.Infrastructure.Data;
using Products.API.Models;
using System.Text.Json;

namespace Products.API.Core.Features.Commands.CreateProduct
{
    public class CreateProductCommandhandler: IRequestHandler<CreateProductCommand, long>
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandhandler(ILogger logger, IConfiguration configuration, IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _repository = repository;
            _unitOfWork= unitOfWork;
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

                var product = _mapper.Map<CreateProductCommand, Product>(command);
                _repository.Insert(product);
                _unitOfWork.Save();
                _logger.LogInformation("create a product, new product id = {ProductId} ", product.ProductId);
                return product.ProductId;

            }
            catch (Exception ex)
            {
                _logger.LogError("Create product handler {ex}", ex);
                return -1;
            }
        }
    }
}
