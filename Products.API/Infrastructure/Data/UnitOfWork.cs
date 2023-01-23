namespace Products.API.Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private readonly IAppDbContextProduct _appDbContextProduct;
        public UnitOfWork(IAppDbContextProduct appDbContextProduct) 
        {
            _appDbContextProduct= appDbContextProduct;
        }

        // This method will save the transaction to the database
        public void Save() => _appDbContextProduct.SaveChanges();

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appDbContextProduct.Dispose();
                }
            }
            disposed = true;

        }

        public void Dispose()
        { 
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
