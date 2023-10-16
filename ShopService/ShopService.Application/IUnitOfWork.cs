using ShopService.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application
{
    public interface IUnitOfWork
    {
        public IOwnerRepository OwnerRepository { get; }
        public IShopRepository ShopRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public Task<bool> SaveChangeAsync();
    }
}
