using ShopService.Application;
using ShopService.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IProductImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UnitOfWork(AppDbContext context,
            IProductImageRepository imageRepository,
            IProductRepository productRepository,
            IOwnerRepository ownerRepository,
            IShopRepository shopRepository,
            ICategoryRepository categoryRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
            _productRepository = productRepository;
            _ownerRepository = ownerRepository;
            _shopRepository = shopRepository;
            _categoryRepository = categoryRepository;
        }

        public IOwnerRepository OwnerRepository => _ownerRepository;

        public IShopRepository ShopRepository => _shopRepository;

        public IProductRepository ProductRepository => _productRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository;

        public IProductImageRepository ProductImageRepository => _imageRepository;

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}
