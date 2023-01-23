using System.Linq.Expressions;

namespace Products.API.Infrastructure.Data
{
    public interface IRepository <TEntity> where TEntity : class
    {
        // Perform the read/get operation.
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        // Perform the read/get operation by id.
        TEntity? GetById(object id);

        // Perform the create/insert operation.
        TEntity Insert(TEntity entity);

        // Perform the update operation
        TEntity Update(TEntity entity);

        // Perform the delete operation.
        void Delete(TEntity entity);

        // Perform the delete operatoin by id
        void Delete(object id);
    }
}
