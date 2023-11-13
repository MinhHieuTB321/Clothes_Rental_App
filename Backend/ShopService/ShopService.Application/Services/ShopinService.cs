using AutoMapper;
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
            => _mapper.Map<IEnumerable<ShopReadModel>>(await _unitOfWork.ShopRepository.GetAllAsync(x=>x.Owner));

        public async Task<Pagination<ProductReadModel>> GetAllProductByShopId(Guid shopId, int pageNumber = 0, int pageSize = 10)
        {
           var pagination = await _unitOfWork.ProductRepository.ToPagination(x=>
                                        x.ShopId==shopId &&
                                        x.IsDeleted==false &&
                                        x.RootProduct==null,
                                        pageNumber, pageSize,
                                        x=>x.Category,x=>x.ProductImages,x=>x.Shop);
			if (pagination.Items.Count == 0) throw new NotFoundException("There are no products in shop!");
			var result = new Pagination<ProductReadModel>
			{
				PageIndex = pagination.PageIndex,
				PageSize = pagination.PageSize,
				TotalItemsCount = pagination.TotalItemsCount,
				Items = _mapper.Map<ICollection<ProductReadModel>>(pagination.Items),
			};
			return result;
		}

        public async Task<ShopReadModel> GetByIdAsync(Guid id)
            => _mapper.Map<ShopReadModel>(await _unitOfWork.ShopRepository.GetByIdAsync(id,x=>x.Owner));

        public async Task<ShopReadModel> UpdateShop(ShopUpdateModel shopUpdateModel)
        {
            var shop = await _unitOfWork.ShopRepository.GetByIdAsync(shopUpdateModel.Id);
            if (shop is null || shop.OwnerId!=_currentUser) throw new Exception("There any shop to update.");
            shop=_mapper.Map(shopUpdateModel, shop);
            if(shopUpdateModel.File !=null){
                var shopLogo = await shopUpdateModel.File!.UploadFileAsync("Shop");
                if (shopLogo != null)
                {
                    shop.FileUrl = shopLogo.URL;
                    shop.FileName = shopLogo.FileName;
                }
            }
            _unitOfWork.ShopRepository.Update(shop);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system.");
            return _mapper.Map<ShopReadModel>(shop);
        }
    }
}
