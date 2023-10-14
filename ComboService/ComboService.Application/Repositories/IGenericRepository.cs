using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //async
        DbSet<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task InsertAsync(TEntity entity);
        void Insert(TEntity entity);
        Task InsertRangeAsync(IQueryable<TEntity> entities);
        void InsertRange(IQueryable<TEntity> entities);

        IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate);
        TEntity Find(Func<TEntity, bool> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int Id);
        Task<TEntity> GetByGuiId(Guid Id);

        Task Update(TEntity entity, int Id);
        Task UpdateGuid(TEntity entity, Guid Id);
        public Task UpdateDetached(TEntity entity);
        Task HardDelete(int Key);
        Task HardDeleteGuid(Guid Key);
        public EntityEntry<TEntity> Delete(TEntity entity);
        void SoftRemove(TEntity entity);

        public IQueryable<TEntity> AsNoTracking();

    }
}
