using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Common
{
    public interface ICrudRepository<TEntity>
    {
         Task<TEntity> GetByIDAsync(object id);
         void Insert(TEntity entity);
         void Delete(object id);
         void Delete(TEntity entityToDelete);
         void Update(TEntity entityToUpdate);
         IQueryable<TEntity> AsQueryable();
    }
}