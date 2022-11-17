using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Common
{
    public class CrudRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;
        public DbSet<TEntity> Entities { get; private set; }

        public CrudRepository(DbContext context)
        {
            _dbContext = context;
            Entities = context.Set<TEntity>();
        }
        public IQueryable<TEntity> AsQueryable() => Entities.AsQueryable();
        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = Entities.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                Entities.Attach(entityToDelete);
            }
            Entities.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            Entities.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}