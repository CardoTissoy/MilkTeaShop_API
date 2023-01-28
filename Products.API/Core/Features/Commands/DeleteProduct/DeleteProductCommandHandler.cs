using MediatR;
using Products.API.Infrastructure.Data;

namespace Products.API.Core.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler: IRequestHandler <DeleteProductCommand, long> 
    {
        private readonly IProductRepository _repository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IProductRepository repository, ILogger<DeleteProductCommandHandler> logger, IUnitOfWork unitOfWork)
        {   
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Delete product {timestamp}.", DateTime.Now);
                var entity = _repository.GetById(command.Id);
                if (entity == null)
                {
                    _logger.LogInformation("Delete product id: {Id} not found", command.Id);
                    return 0;
                }
                if (entity.ProductId > 0)
                {
                    _repository.Delete(entity.ProductId);
                    _unitOfWork.Save();
                }
                _logger.LogInformation("Delete product ok()");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete product id: {ex}", ex);
                return -1;
            }

           
        }
    }
}
