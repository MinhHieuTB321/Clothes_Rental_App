using Microsoft.AspNetCore.Http;
using ShopService.Application.ViewModels.Images;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface IProductImageService
    {
        public Task<ProductImage?> AddImageAsync(IFormFile file, Guid productId);


        public Task<bool> DeleteImage(Guid id);
        public Task<IEnumerable<ImageReadModel>> GetAll();
        public Task<ImageReadModel> GetById(Guid id);
    }
}
