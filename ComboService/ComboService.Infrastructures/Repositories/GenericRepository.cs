using ComboService.Application.Repositories;
using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private static ApplicationDbContext Context;
        private static DbSet<T> Table { get; set; }

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }
        public DbSet<T> GetAll()
        {
            return Table;
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
           await Table.AddAsync(entity);
        }

        public void Insert(T entity)
        {
            Table.Add(entity);
        }

        public async Task InsertRangeAsync(IQueryable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public  void InsertRange(IQueryable<T> entities)
        {
            Table.AddRange(entities);
        }

        public IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        public T Find(Func<T, bool> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.SingleOrDefaultAsync(predicate);
        }
        public async Task<T> GetById(int Id)
        {
            return await Table.FindAsync(Id);
        }

        public async Task<T> GetByGuiId(Guid Id)
        {
            return await Table.FindAsync(Id);
        }

        public async Task Update(T entity, int Id)
        {
            var existEntity = await GetById(Id);
            Context.Entry(existEntity).CurrentValues.SetValues(entity);
            Table.Update(existEntity);
        }

        public async Task UpdateGuid(T entity, Guid Id)
        {
            var existEntity = await GetByGuiId(Id);
            Context.Entry(existEntity).CurrentValues.SetValues(entity);
            Table.Update(existEntity);
        }

        public async Task UpdateDetached(T entity)
        {
            Table.Update(entity);
        }
        public async Task HardDelete(int Key)
        {
            var rs = await GetById(Key);
            Table.Remove(rs);
        }

        public async Task HardDeleteGuid(Guid Key)
        {
            var rs = await GetByGuiId(Key);
            Table.Remove(rs);
        }

        public EntityEntry<T> Delete(T entity)
        {
            return Table.Remove(entity);
        }

        public IQueryable<T> AsNoTracking()
        {
            return Table.AsNoTracking();
        }

        public void SoftRemove(T entity)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
        }


    }
}
