using AutoMapper;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Services
{
    public class ShopService : IShopService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShopService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ShopResponseModel>> GetShops()
        {
            var shops = await _unitOfWork.Repository<Shop>().GetAll().ToListAsync();
            if (shops.Count > 0)
            {
                shops = shops.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopResponseModel>>(shops);
            }
            else throw new Exception("Not have any shops");
        }
    }
}
