using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.IRepositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
    }
}
