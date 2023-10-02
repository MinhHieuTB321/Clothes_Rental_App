using AutoMapper;
using ShopService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class Shop_Service:IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Guid _currentUser;

        public Shop_Service(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimService.GetCurrentUser;
        }
    }
}
