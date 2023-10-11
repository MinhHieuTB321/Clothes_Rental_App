using AutoMapper;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Categories;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper,IClaimService claimService)
        {
            _unitOfWork= unitOfWork;    
            _mapper= mapper;
            _currentUser = claimService.GetCurrentUser;
        }

        public async Task<CategoryReadModel> CreateCategory(CategoryCreateModel categoryCreateModel)
        {
            var map = _mapper.Map<Category>(categoryCreateModel);
            if (categoryCreateModel.CategoryName == null) throw new Exception("No information has been entered!");
            await _unitOfWork.CategoryRepository.AddAsync(map);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in system");
            return _mapper.Map<CategoryReadModel>(map);
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is not null)
            {
                _unitOfWork.CategoryRepository.SoftRemove(category);
                if (await _unitOfWork.SaveChangeAsync())
                    return true;
                else return false;
            }
            else throw new Exception($"Not found Category with Id : {id}");
        }

        public async Task<IEnumerable<CategoryReadModel>> GetAllAsync() =>
            _mapper.Map<IEnumerable<CategoryReadModel>>(await _unitOfWork.CategoryRepository.GetAllAsync());

        public async Task<CategoryReadModel> GetByIdAsync(Guid id)
            => _mapper.Map<CategoryReadModel>(await _unitOfWork.CategoryRepository.GetByIdAsync(id));

        public async Task<CategoryReadModel> UpdateCategory(CategoryUpdateModel categoryUpdateModel)
        {
            var category =await _unitOfWork.CategoryRepository.GetByIdAsync(categoryUpdateModel.Id);
            if(category is not null)
            {
                _mapper.Map(category, categoryUpdateModel);
                _unitOfWork.CategoryRepository.Update(category);
                if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in system");
                return _mapper.Map<CategoryReadModel>(category);
            }
            else throw new Exception("There is no category to update!");
        }
    }
}
