using AutoMapper;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Products;
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

        public Task<ProductReadModel> CreateProduct(ProductCreateModel productCreateModel)
        {
            throw new NotImplementedException();
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
        => _mapper.Map<IEnumerable<ProductReadModel>>(await _unitOfWork.ProductRepository.GetAllAsync());

        public async Task<ProductReadModel> GetByIdAsync(Guid id)
        => _mapper.Map<ProductReadModel>(await _unitOfWork.ProductRepository.GetByIdAsync(id));

        public async Task<ProductReadModel> UpdateProduct(ProductUpdateModel productUpdateModel)
        {
            var product=await _unitOfWork.ProductRepository.GetByIdAsync(productUpdateModel.Id);
            if (product is null || product.Shop.OwnerId!=_currentUser) throw new Exception("There is no product to update.");
            _mapper.Map(productUpdateModel,product);
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangeAsync()? _mapper.Map<ProductReadModel>(product): null;
        }
    }
}
