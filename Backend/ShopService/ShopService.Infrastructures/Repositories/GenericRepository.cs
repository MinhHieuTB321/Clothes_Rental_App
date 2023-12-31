﻿using Microsoft.EntityFrameworkCore;
using ShopService.Application.Commons;
using ShopService.Application.Interfaces;
using ShopService.Application.IRepositories;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimService _claimService;

        public GenericRepository(AppDbContext context, IClaimService claimsService, ICurrentTime currentTime)
        {
            _dbSet = context.Set<TEntity>();
            _claimService = claimsService;
            _timeService = currentTime;
        }


        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes) =>
            await includes
           .Aggregate(_dbSet.AsQueryable(),
               (entity, property) => entity.Include(property).IgnoreAutoIncludes())
           .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreationDate)
           .ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
               .Aggregate(_dbSet.AsQueryable(),
                   (entity, property) => entity.Include(property))
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreationDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimService.GetCurrentUser;
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteBy = _claimService.GetCurrentUser;
            entity.DeletionDate = _timeService.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.ModificationDate = _timeService.GetCurrentTime();
            entity.ModificationBy = _claimService.GetCurrentUser;
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimService.GetCurrentUser;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimService.GetCurrentUser;
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task<Pagination<TEntity>> ToPagination(Expression<Func<TEntity, bool>> expression,int pageNumber = 0, int pageSize = 10, params Expression<Func<TEntity, object>>[] includes)
        {
            var itemCount = await includes
                            .Aggregate(_dbSet!.AsQueryable(),
                                (entity, property) => entity.Include(property)).AsNoTracking()
                            .Where(expression!).CountAsync();
            var items =await includes
                                    .Aggregate(_dbSet!.AsQueryable(),
                                        (entity, property) => entity.Include(property)).AsNoTracking()
                                    .Where(expression!)
                                        .OrderByDescending(x => x.CreationDate)
                                    .Skip(pageNumber * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public void UpdateRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimService.GetCurrentUser;
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task<TEntity> FindByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var result=await includes
           .Aggregate(_dbSet!.AsQueryable()!,
               (entity, property) => entity!.Include(property)).AsNoTracking()
           .Where(expression!)
            .FirstOrDefaultAsync(x => x.IsDeleted == false);
            return result!;
        }

        public async Task<List<TEntity>> FindListByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        => await includes
           .Aggregate(_dbSet!.AsQueryable(),
               (entity, property) => entity.Include(property)).AsNoTracking()
           .Where(expression!)
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync();

        public void Delete(TEntity entity){
            _dbSet.Remove(entity);
        }
    }
}
