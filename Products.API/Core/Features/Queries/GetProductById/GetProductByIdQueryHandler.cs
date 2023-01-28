using AutoMapper;
using MediatR;
using Products.API.Core.DTOs;
using Products.API.Infrastructure.Data;
using Products.API.Models;
using System.Text.Json;

namespace Products.API.Core.Features.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdVm>
    {
        private readonly IProductRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public GetProductByIdQueryHandler(IProductRepository repository, ILogger logger, IMapper mapper, IConfiguration configuration)
        {
            _repository= repository;
            _logger= logger;
            _mapper= mapper;
            _configuration = configuration;
        }


        public async Task<GetProductByIdVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _repository.GetById(request.Id);
                _logger.LogInformation("Get product by id {Id}: {timestamp}", request.Id, DateTime.Now);
                if (product == null)
                {
                    _logger.LogInformation("Get product by id = {Id} not found.", request.Id);
                    return new GetProductByIdVm();
                }

                var productDto = _mapper.Map<Product, ProductDto>(product);
                var vm = new GetProductByIdVm
                {
                    Product = productDto,
                };
                if (_configuration.GetValue<bool>("EnableDetailedLog"))
                {
                    var prod = JsonSerializer.Serialize(vm);
                    _logger.LogInformation("Get product by id ok() : {prod}.", prod);
                }

                return vm;
            }
            catch (Exception ex)
            {
                _logger.LogError("Get product by id = {ex}", ex);
                return new GetProductByIdVm();
            }
           
        }
    }
}
