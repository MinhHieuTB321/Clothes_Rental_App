using ComboService.Application;
using ComboService.Application.Repositories;
using ComboService.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IGenericRepository<T>? Repository<T>() where T : class
        {
            Type type = typeof(T);
            if(!repositories.TryGetValue(type, out object? value))
            {
                var genericRepos = new GenericRepository<T>(_context);
                repositories.Add(type, genericRepos);
                return genericRepos;
            }
            return value as GenericRepository<T>;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
