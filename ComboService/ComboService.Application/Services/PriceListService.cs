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
            try
            {
                var checkList = _unitOfWork.Repository<PriceList>().GetAll().Where(x => x.ComboId.Equals(request.ComboId));
                var checkPriceList = checkList.Where(x => x.Duration.Equals(request.Duration))
                                .SingleOrDefault();
                if(checkPriceList != null)
                {
                    throw new Exception("This price list is already exist!");
                }
                PriceList priceList = new PriceList();
                priceList.RentalPrice = request.RentalPrice;
                priceList.ComboId = request.ComboId;
                priceList.Duration = request.Duration;
                priceList.Deposit = request.Deposit;

                #region Combo
                Combo combo = new Combo();
                combo = await _unitOfWork.Repository<Combo>().FindAsync(x => x.Id.Equals(request.ComboId));
                combo.PriceLists.Add(priceList);
                #endregion

                await _unitOfWork.Repository<Combo>().UpdateDetached(combo);
                await _unitOfWork.Repository<PriceList>().InsertAsync(priceList);

                await _unitOfWork.CommitAsync();
                return _mapper.Map<PriceList, PriceListResponseModel>(priceList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<IEnumerable<PriceListResponseModel>> GetPriceLists()
        {
            var priceLists = _unitOfWork.Repository<PriceList>().GetAll().ToList();
            if (priceLists.Count > 0)
            {
                priceLists = priceLists.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<PriceList>, IEnumerable<PriceListResponseModel>>(priceLists);
            }
            else throw new Exception("Not have any pricelist");
        }

        public async Task<PriceListResponseModel> GetPriceListByGuid(Guid Id)
        {
            var checkPriceList = await _unitOfWork.Repository<PriceList>().GetAll().Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
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
                priceList = _unitOfWork.Repository<PriceList>().Find(x => x.Id.Equals(Id));
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
