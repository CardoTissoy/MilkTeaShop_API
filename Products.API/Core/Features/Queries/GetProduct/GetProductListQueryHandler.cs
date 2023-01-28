using AutoMapper;
using MediatR;
using Products.API.Core.DTOs;
using Products.API.Infrastructure.Data;
using Products.API.Models;
using System.Text.Json;

namespace Products.API.Core.Features.Queries.GetProduct
{
    // This class will perform the read functionality for the Product API.
    public class GetProductListQueryHandler: IRequestHandler<GetProductListQuery, ProductListVm>
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GetProductListQueryHandler(IProductRepository repository, ILogger logger, IMapper mapper, IConfiguration configuration)
        {
            _repository= repository;
            _logger= logger;
            _mapper= mapper;
            _configuration= configuration;
        }

        // The method will retrieve all products
        public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allProducts = _repository.Get().ToList();
                if (!allProducts.Any())
                {
                    _logger.LogInformation("Products does not exist in the list");
                    return new ProductListVm();
                }

                var products = _mapper.Map<List<Product>, List<ProductDto>>(allProducts);
                var vm = new ProductListVm
                {
                    Products = products
                };

                if (_configuration.GetValue<bool>("EnableDetailedLog"))
                {
                    var serializeProducts = JsonSerializer.Serialize(vm);
                    _logger.LogInformation("Get all products ok() {serializeProducts}", serializeProducts);
                }
                return vm;
            }
            catch(Exception ex) 
            {
                _logger.LogError("Get all products handler", ex);
                return new ProductListVm();
            }
           

        }

    }
}
