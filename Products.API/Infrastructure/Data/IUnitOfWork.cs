namespace Products.API.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        // Save the data to the database
        void Save();

        // Dispose the database context
        void Dispose(bool disposing);

        // Dispose the database context
        void Dispose();
    }
}
