using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopService.Application.Commons;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Images;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimService.GetCurrentUser;
        }
        public async Task<ImageReadModel> AddImageAsync(ImageCreateModel model)
        {
            var fireBaseFile = await model.File.UploadFileAsync("Product");
            if (fireBaseFile is not null)
            {
                var productImage = new ProductImage()
                {
                    FileName = fireBaseFile.FileName,
                    FileUrl = fireBaseFile.URL,
                    ProductId = model.ProductId
                };
                var result= await _unitOfWork.ProductImageRepository.AddAsync(productImage);
                if (await _unitOfWork.SaveChangeAsync()) return (_mapper.Map<ImageReadModel>(result));

            }
            throw new NotImplementedException("Error occured at Upload file!");
        }

        public async Task<bool> DeleteImage(Guid id)
        {
            var deletedItem = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (deletedItem != null)
            {
                _unitOfWork.ProductImageRepository.Delete(deletedItem);
                var result = await deletedItem.FileName.RemoveFileAsync("Product");
                await _unitOfWork.SaveChangeAsync();
                if (result) return true;
                else throw new Exception("Remove File at Firebase occured");
            }
            else throw new Exception("Not found");
        }

        public async Task<IEnumerable<ImageReadModel>> GetAll()
            => _mapper.Map<IEnumerable<ImageReadModel>>(await _unitOfWork.ProductImageRepository.GetAllAsync());

        public async Task<ImageReadModel> GetById(Guid id)
            => _mapper.Map<ImageReadModel>(await _unitOfWork.ProductImageRepository.GetByIdAsync(id));
    }
}
