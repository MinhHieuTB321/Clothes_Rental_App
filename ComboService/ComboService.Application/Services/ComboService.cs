using AutoMapper;
using ComboService.Application.Commons;
using ComboService.Application.GlobalExceptionHandling;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComboService.Application.Services
{
    public class CombosService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CombosService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ComboResponseModel> CreateCombo(CreateComboRequestModel request)
        {
            var checkCombo = _unitOfWork.Repository<Combo>().Find(x => x.ComboName.Equals(request.ComboName));
            if (checkCombo != null)
            {
                throw new Exception("Combo all ready exist!!!");
            }
            var combo= _mapper.Map<Combo>(request);
            var fileResponse= await request.File.UploadFileAsync("Combo");
            combo.FileName=fileResponse.FileName;
            combo.FileUrl=fileResponse.URL;

            await _unitOfWork.Repository<Combo>().InsertAsync(combo);
            await _unitOfWork.CommitAsync();

            var result= _mapper.Map<ComboResponseModel>(combo);
            return result;
        }

        public async Task<ComboResponseModel> DeleteCombo(Guid Id)
        {
            try
            {
                var combo = await _unitOfWork.Repository<Combo>().FindAsync(x => x.Id.Equals(Id));
                if(combo == null)
                {
                    throw new NotFoundException("Combo is not exist!");
                }
                _unitOfWork.Repository<Combo>().SoftRemove(combo);
                await _unitOfWork.CommitAsync();
                var result= _mapper.Map<Combo, ComboResponseModel>(combo);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ComboResponseModel> GetComboByGuid(Guid Id)
        {
            var combo = await _unitOfWork.Repository<Combo>()
                .GetAll()
                .Where(x => x.Id.Equals(Id))
                .Include(x=>x.Shop)
                .Include(x=>x.PriceLists)
                .FirstOrDefaultAsync();
            if (combo is not null)
            {
                var result= _mapper.Map<Combo, ComboResponseModel>(combo);
                result.ProductCombos=await GetProductComboByComboId(combo.Id);
                return result;
            }
            else throw new Exception("Not found");
        }


        private async Task<List<ProductComboResponseModel>> GetProductComboByComboId(Guid Id)
        {
            var productCombos = await _unitOfWork.Repository<ProductCombo>()
                .GetAll()
                .Where(x => x.ComboId.Equals(Id))
                .Include(x=>x.Product)
				.ThenInclude(x => x.ProductImages)
				.Include(x=>x.Combo)
                .ThenInclude(x=>x.Shop)
                .ToListAsync();
            if (!productCombos.Any()) return null;
            return _mapper.Map<List<ProductComboResponseModel>>(productCombos);
        }

        public async Task<IEnumerable<ComboResponseModel>> GetCombos()
        {
           var combos = await _unitOfWork.Repository<Combo>()
                .GetAll()
                .Include(x=>x.Shop)
                .ToListAsync();
            if (combos.Count > 0)
            {
                combos = combos.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<Combo>, IEnumerable<ComboResponseModel>>(combos);
            }
            else throw new Exception("Not have any combo");

        }

        public async Task<ComboResponseModel> UpdateCombo(Guid Id, UpdateComboRequestModel request)
        {
            Combo  combo = _unitOfWork.Repository<Combo>().Find(x => x.Id.Equals(Id)&&x.IsDeleted==false);
            if(combo == null)
            {
                throw new NotFoundException("Combo is not exist!");
            }
            combo=_mapper.Map(request, combo);
            var fileResponse= await request.File.UploadFileAsync("Combo");
            combo.FileName=fileResponse.FileName;
            combo.FileUrl=fileResponse.URL;
            await _unitOfWork.Repository<Combo>().UpdateDetached(combo);
            await _unitOfWork.CommitAsync();
            var result= _mapper.Map<Combo, ComboResponseModel>(combo);
            return result;
        }


		public async Task<List<ProductComboResponseModel>> AddProductCombo(Guid comboId,List<ProductComboRequestModel> request)
		{
			var combo = await _unitOfWork.Repository<Combo>()
			  .GetAll().Where(x => x.Id.Equals(comboId)&& x.IsDeleted==false).FirstOrDefaultAsync();
			if (combo is null) throw new Exception($"Not found combo with Id-{comboId}");
			var productCombos = _mapper.Map<List<ProductCombo>>(request);
			await _unitOfWork.Repository<ProductCombo>().InsertRangeAsync(productCombos.AsQueryable());
			await _unitOfWork.CommitAsync();
            await UpdateTotal(combo);
			return _mapper.Map<List<ProductComboResponseModel>>(productCombos);
		}

        private async Task UpdateTotal(Combo combo)
        {
			var productCombos = await _unitOfWork.Repository<ProductCombo>()
			   .GetAll()
			   .Where(x => x.ComboId.Equals(combo.Id))
			   .Include(x => x.Product)
			   .ToListAsync();
			var comboResponse = _mapper.Map<Combo, ComboResponseModel>(combo);
            comboResponse.TotalValue = productCombos.Sum(x => x.Product.Price);
            combo.TotalValue=comboResponse.TotalValue;
			await _unitOfWork.Repository<Combo>().UpdateDetached(combo);
			await _unitOfWork.CommitAsync();
		}
	}
}
