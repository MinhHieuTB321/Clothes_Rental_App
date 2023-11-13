using ShopService.Application.Commons;
using ShopService.Domain.Entities;
using System.Linq.Expressions;

namespace ShopService.Application.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void SoftRemove(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void SoftRemoveRange(List<TEntity> entities);

        void Delete(TEntity entity);
        Task<TEntity> FindByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

        Task<Pagination<TEntity>> ToPagination(Expression<Func<TEntity, bool>> expression,int pageNumber = 0, int pageSize = 10, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> FindListByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
       
    }
}
