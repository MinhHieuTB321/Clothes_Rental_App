﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopService.Application.Commons;
using ShopService.Application.GlobalExceptionHandling.Exceptions;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Products;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimService.GetCurrentUser;
        }

        public async Task<ProductReadModel> CreateProduct(ProductCreateModel productCreateModel)
        {
            var map = _mapper.Map<Product>(productCreateModel);
            var result = await _unitOfWork.ProductRepository.AddAsync(map);
            await AddImageAsync(productCreateModel.File!,result.Id);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system.");
            return _mapper.Map<ProductReadModel>(result);

        }
        public async Task AddImageAsync(IEnumerable<IFormFile> files, Guid productId)
        {
           foreach (var item in files)
           {
                var fireBaseFile = await item.UploadFileAsync("Product");
                if (fireBaseFile is not null)
                {
                    var productImage = new ProductImage()
                    {
                        FileName = fireBaseFile.FileName,
                        FileUrl = fireBaseFile.URL,
                        ProductId = productId
                    };
                    await _unitOfWork.ProductImageRepository.AddAsync(productImage);
                }
           }
        }
        
        public async Task<bool> DeleteProduct(Guid id)
        {
            var product=await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null) throw new Exception("There no product to delete.");
            _unitOfWork.ProductRepository.SoftRemove(product);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system.");
            else return true;    
        }

        public async Task<IEnumerable<ProductReadModel>> GetAllAsync()
        {
            
            var result = _mapper.Map<IEnumerable<ProductReadModel>>(await _unitOfWork.ProductRepository.FindListByField(x=>x.RootProductId==null&& x.IsDeleted==false,x=>x.ProductImages,x=>x.Shop,x=>x.Category));
            return result;
        }

        public async Task<ProductReadModel> GetByIdAsync(Guid id)
        {
            var result = _mapper.Map<ProductReadModel>(await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.ProductImages, x => x.Shop, x => x.Category,x=>x.SubProducts!));
            return result;
        }

        public async Task<bool> UpdateProduct(ProductUpdateModel productUpdateModel)
        {
            var product=await _unitOfWork.ProductRepository.GetByIdAsync(productUpdateModel.Id);
            if (product is null || product.Shop.OwnerId!=_currentUser) throw new Exception("There is no product to update.");
            _mapper.Map(productUpdateModel,product);
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<ProductReadModel>> GetAllSubProductByRootId(Guid id)
        {
            var result = _mapper.Map<List<ProductReadModel>>(await _unitOfWork.ProductRepository.FindListByField(x=>x.RootProductId==id&& x.IsDeleted==false,x=>x.ProductImages,x=>x.Shop,x=>x.Category));
            if(result.Count==0) throw new NotFoundException($"There are no sub-product for Id-{id}!");
            return result;
        }
    }
}
