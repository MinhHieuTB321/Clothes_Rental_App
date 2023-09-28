using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //async
        DbSet<TEntity> GetAll();

    }
}
