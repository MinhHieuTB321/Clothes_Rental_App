﻿using AutoMapper;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComboService.Application.Services
{
    public class ProductComboService : IProductComboService
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductComboService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductComboResponseModel>> Create(List<ProductComboRequestModel> request)
        {
            var productCombos= _mapper.Map<List<ProductCombo>>(request);
            await _unitOfWork.Repository<ProductCombo>().InsertRangeAsync(productCombos.AsQueryable());
            await _unitOfWork.CommitAsync();

            return _mapper.Map<List<ProductComboResponseModel>>(productCombos);
        }

		public async Task<bool> Delete(Guid id)
		{
			var productCombo = await _unitOfWork.Repository<ProductCombo>().FindAsync(x => x.Id == id);
			if (productCombo == null) throw new Exception("Product is not exist!");
            productCombo.IsDeleted = true;
			await _unitOfWork.Repository<ProductCombo>().UpdateDetached(productCombo);
			return await _unitOfWork.CommitAsync() > 0;
		}

		public async Task<List<ProductComboResponseModel>> GetAllByComboId(Guid comboId)
		{
			var productCombos = await _unitOfWork.Repository<ProductCombo>()
				.GetAll()
				.Where(x => x.ComboId.Equals(comboId))
				.Include(x => x.Product)
				.ThenInclude(x => x.ProductImages)
				.Include(x => x.Combo)
				.ThenInclude(x => x.Shop)
				.ToListAsync();
			if (!productCombos.Any()) throw new Exception($"There are no products in combo with ID - {comboId}");
			return _mapper.Map<List<ProductComboResponseModel>>(productCombos);
		}

		public async Task<ProductComboResponseModel> GetProductComboById(Guid id)
		{
			var productCombo= await _unitOfWork.Repository<ProductCombo>()
				.GetAll()
				.Where(x => x.Id==id)
				.Include(x => x.Product)
				.ThenInclude(x => x.ProductImages)
				.Include(x => x.Combo)
				.ThenInclude(x => x.Shop)
				.FirstOrDefaultAsync();
				

			if (productCombo == null) throw new Exception("Product is not exist!");
			return _mapper.Map<ProductComboResponseModel>(productCombo);
		}

		public async Task<bool> Update(UpdateProductComboRequest request)
		{
			var productCombo= await _unitOfWork.Repository<ProductCombo>().FindAsync(x=>x.Id==request.Id);
            if (productCombo == null) throw new Exception("Product is not exist!");
            productCombo= _mapper.Map<ProductCombo>(request);
            await _unitOfWork.Repository<ProductCombo>().UpdateDetached(productCombo);
            return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
