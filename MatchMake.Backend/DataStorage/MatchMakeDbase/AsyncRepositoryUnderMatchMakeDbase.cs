using MatchMake.Backend.DataStorage.MatchMake.Context;
using MatchMake.Backend.DataStorage.Shared;
using MatchMake.Backend.Storage;
using MatchMake.Backend.Storage.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MatchMake.Backend.DataStorage.MatchMakeDbase
{
    public class AsyncRepositoryUnderMatchMakeDbase<TEntity, TId> : IAsyncRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {
        private readonly DbContext _context;

        public AsyncRepositoryUnderMatchMakeDbase(MatchMakeContext matchMakeContext)
        {
            _context = matchMakeContext;
        }

        #region Current Class Methods

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Save(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public async Task SaveAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity target)
        {
            _context.Entry(target).State = EntityState.Deleted;

            _context.SaveChanges();
        }

        public TEntity GetByCustomQuery(ICustomUserQuery<TEntity> userQuery)
        {
            return GetAll().Where(userQuery.IsSatisfiedBy()).SingleOrDefault();
        }

        #endregion

        #region Contracts From IAsync Interface

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> GetByCustomQueryAsync(ICustomUserQuery<TEntity> customQuery)
        {
            return await GetAll().Where(customQuery.IsSatisfiedBy()).SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> GetAllByCustomQueryAsListAsync(ICustomUserQuery<TEntity> customQuery)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}