using AutoMapper;
using ComboService.Application.GlobalExceptionHandling;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
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
    public class PriceListService : IPriceListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PriceListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PriceListResponseModel> CreatePriceList(CreatePriceListRequestModel request)
        {
           var checkPrice= await _unitOfWork.Repository<PriceList>()
                .GetAll()
                .FirstOrDefaultAsync(x=>x.ComboId==request.ComboId && x.Duration.Equals(request.Duration));
            if (checkPrice != null) throw new Exception("Duration is already exist!");
            var price= _mapper.Map<PriceList>(request);
            await _unitOfWork.Repository<PriceList>().InsertAsync(price);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PriceListResponseModel>(price);
        }

        public async Task<PriceListResponseModel> DeletePriceList(Guid Id)
        {
            try
            {
                var priceList = await _unitOfWork.Repository<PriceList>().FindAsync(x => x.Id.Equals(Id));
                if(priceList == null)
                {
                    throw new NotFoundException("Price List is not exist!");
                }
                _unitOfWork.Repository<PriceList>().SoftRemove(priceList);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<PriceList, PriceListResponseModel>(priceList);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PriceListResponseModel>> GetPriceLists(Guid comboId)
        {
            var priceLists = _unitOfWork.Repository<PriceList>().GetAll().Where(x=>x.IsDeleted==false&&x.ComboId==comboId).ToList();
            if (priceLists.Count > 0)
            {
                priceLists = priceLists.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<PriceList>, IEnumerable<PriceListResponseModel>>(priceLists);
            }
            else throw new Exception("Not have any pricelist");
        }

        public async Task<PriceListResponseModel> GetPriceListByGuid(Guid Id)
        {
            var checkPriceList = await _unitOfWork.Repository<PriceList>().GetAll().Where(x => x.Id.Equals(Id)&&x.IsDeleted==false).FirstOrDefaultAsync();
            if (checkPriceList is not null)
            {
                return _mapper.Map<PriceList, PriceListResponseModel>(checkPriceList);
            }
            else throw new Exception("Not Found");
        }

        public async Task<PriceListResponseModel> UpdatePriceList(Guid Id, UpdatePriceListRequestModel request)
        {
            try
            {
                PriceList priceList = null;
                priceList = _unitOfWork.Repository<PriceList>().Find(x => x.Id.Equals(Id)&&x.IsDeleted==false);
                if(priceList == null)
                {
                    throw new NotFoundException("Price list is not exist!");
                }
                _mapper.Map<UpdatePriceListRequestModel, PriceList>(request, priceList);
                await _unitOfWork.Repository<PriceList>().UpdateDetached(priceList);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<PriceList, PriceListResponseModel>(priceList);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
