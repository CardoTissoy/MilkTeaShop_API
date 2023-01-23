using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Products.API.Infrastructure.Data
{
    public interface IAppDbContextProduct
    {
        // Perform to set the entity.
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        // Peform to save changes.
        int SaveChanges();

        // Perform to set the entity entry.
        EntityEntry Entry(object o);

        // Perform to set the entity to modified.
        void SetModified(object entity);

        // Perform to dipose the database context.
        void Dispose();
    }
}
