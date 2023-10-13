using AutoMapper;
using ShopService.Application.Interfaces;
using ShopService.Application.Utils;
using ShopService.Application.ViewModels.Owners;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class OwnerService:IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimService.GetCurrentUser;
        }

        public async Task<OwnerReadModel> CreteOwner(OwnerCreateModel ownerCreateModel)
        {
            var map = _mapper.Map<Owner>(ownerCreateModel);
            map.Status = "Active";
            if (!ValidationLibrary.IsSpace(ownerCreateModel.Name)) throw new Exception("Have not inputted Name!");
            if (!ValidationLibrary.IsSpace(ownerCreateModel.Email)) throw new Exception("Have not inputted Email!");
            if (!ValidationLibrary.IsValidEmail(ownerCreateModel.Email)) throw new Exception("Invalid email.");
            var duplicatedEmail = await _unitOfWork.OwnerRepository.FindByField(x => x.Email.Equals(ownerCreateModel.Email));
            if (duplicatedEmail != null) throw new Exception("Email is already in use.");
            if (!ValidationLibrary.IsSpace(ownerCreateModel.Phone)) throw new Exception("Have not inputted Phone!");
            await _unitOfWork.OwnerRepository.AddAsync(map);
            if(!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system");
            return _mapper.Map<OwnerReadModel>(map);
        }

        public async Task<bool> DeleteOwner(Guid id)
        {
            var owner =await _unitOfWork.OwnerRepository.GetByIdAsync(id);
            if (owner is null) throw new Exception("Not found!");
            _unitOfWork.OwnerRepository.SoftRemove(owner);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system");
            return true;
        }

        public async Task<IEnumerable<OwnerReadModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<OwnerReadModel>>(await _unitOfWork.OwnerRepository.GetAllAsync());

        public async Task<OwnerReadModel> GetByIdAsync(Guid id)
            => _mapper.Map<OwnerReadModel>(await _unitOfWork.OwnerRepository.GetByIdAsync(id));

        public async Task<OwnerReadModel> UpdateOwner(OwnerUpdateModel ownerUpdateModel)
        {
            var owner = await _unitOfWork.OwnerRepository.GetByIdAsync(ownerUpdateModel.Id);
            if (owner is null) throw new Exception("There is no owner to update");
            var map = _mapper.Map(ownerUpdateModel, owner);
            _unitOfWork.OwnerRepository.Update(owner);
            if (!await _unitOfWork.SaveChangeAsync()) throw new Exception("There is an error in the system");
            return _mapper.Map<OwnerReadModel>(owner);
        }

        public Task<bool> UpdateStatusOwner(OwnerUpdateStatusModel ownerUpdateStatusModel)
        {
            throw new NotImplementedException();
        }
    }
}
