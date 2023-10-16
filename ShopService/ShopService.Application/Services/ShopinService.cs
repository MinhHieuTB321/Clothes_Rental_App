﻿using AutoMapper;
using ShopService.Application.Commons;
using ShopService.Application.GlobalExceptionHandling.Exceptions;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Products;
using ShopService.Application.ViewModels.Shops;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class ShopinService:IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public ShopinService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimService.GetCurrentUser;
        }

        public async Task<ShopReadModel> CreateShop(ShopCreateModel shopCreateModel)
        {
            var map = _mapper.Map<Shop>(shopCreateModel);
            map.OwnerId= _currentUser;
            var shopLogo = await shopCreateModel.File.UploadFileAsync("Shop");
            if(shopLogo != null)
            {
                map.FileUrl = shopLogo.URL;
                map.FileName = shopLogo.FileName;
            }
            await _unitOfWork.ShopRepository.AddAsync(map);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system");
            return _mapper.Map<ShopReadModel>(map);
        }

        public async Task<bool> DeleteShop(Guid shopId)
        {
            var shop = await _unitOfWork.ShopRepository.GetByIdAsync(shopId);
            if (shop is not null) 
                _unitOfWork.ShopRepository.SoftRemove(shop);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<IEnumerable<ShopReadModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<ShopReadModel>>(await _unitOfWork.ShopRepository.FindListByField(x=>x.OwnerId==_currentUser,x=>x.Owner));

        public async Task<List<ProductReadModel>> GetAllProductByShopId(Guid shopId)
        {
           var result= await _unitOfWork.ProductRepository.FindListByField(x=>
                                        x.ShopId==shopId &&
                                        x.IsDeleted==false &&
                                        x.RootProduct==null,
                                        x=>x.Category,x=>x.ProductImages,x=>x.Shop);
            if(result.Count==0) throw new NotFoundException("There are no products in shop!");
            return _mapper.Map<List<ProductReadModel>>(result);
        }

        public async Task<ShopReadModel> GetByIdAsync(Guid id)
            => _mapper.Map<ShopReadModel>(await _unitOfWork.ShopRepository.GetByIdAsync(id,x=>x.Owner));

        public async Task<ShopReadModel> UpdateShop(ShopUpdateModel shopUpdateModel)
        {
            var shop = await _unitOfWork.ShopRepository.GetByIdAsync(shopUpdateModel.Id);
            if (shop is null || shop.OwnerId!=_currentUser) throw new Exception("There any shop to update.");
            _mapper.Map(shopUpdateModel, shop);
            var shopLogo = await shopUpdateModel.File.UploadFileAsync("Shop");
            if (shopLogo != null)
            {
                shop.FileUrl = shopLogo.URL;
                shop.FileName = shopLogo.FileName;
            }
            _unitOfWork.ShopRepository.Update(shop);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system.");
            return _mapper.Map<ShopReadModel>(shop);
        }
    }
}