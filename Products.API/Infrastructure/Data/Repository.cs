using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Products.API.Infrastructure.Data
{
    // Perform the CRUD operations and data logic.
    public abstract class Repository<TEntity>: IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly IAppDbContextProduct _dbContextProduct;
        protected Repository(IAppDbContextProduct appDbContextProduct) 
        {
            _dbContextProduct = appDbContextProduct;
            dbSet = _dbContextProduct.Set<TEntity>();
        }

        // This method will perform the read/get operation.
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        // This method will perform the read/get operation by id.
        public virtual TEntity? GetById(object id)
        {
             var entity = dbSet.Find(id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        // This method will perform the create operation.
        public virtual TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        //  Perform the update operation.
        public virtual TEntity Update(TEntity entity)
        {
            dbSet.Attach(entity);
            if (_dbContextProduct.Entry != null)
            {
                _dbContextProduct.Entry(entity).State= EntityState.Modified;
            }
            return entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        //Perform the delete operation
        public virtual void Delete(TEntity entity)
        {
            if (_dbContextProduct.Entry(entity) != null && _dbContextProduct.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            } 
            dbSet.Remove(entity);
        }
    }
}
