using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Common
{
    public abstract class BaseUnitOfWork<TDbContext> : IDisposable where TDbContext : DbContext
    {
        protected TDbContext _dbContext;

        public BaseUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void ClearChanges()
        {
            _dbContext.ChangeTracker.Clear();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}