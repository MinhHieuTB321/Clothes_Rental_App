using AutoMapper;
using ShopService.Application.Interfaces;
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
    }
}
