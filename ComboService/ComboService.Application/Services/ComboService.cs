using AutoMapper;
using ComboService.Application.GlobalExceptionHandling;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using ComboService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var checkCombo = _unitOfWork.Repository<Combo>().Find(x => x.ComboName.Equals(request.ComboName));
                if (checkCombo != null)
                {
                    throw new Exception("Combo all ready exist!!!");
                }

                Combo combo = new Combo();
                combo.ComboName = request.ComboName;
                combo.Quantity = request.Quantity;
                combo.Status = request.Status;
                combo.ShopId = request.ShopId;
                combo.Shop = _unitOfWork.Repository<Shop>().Find(x => x.Id.Equals(request.ShopId));

                #region Product Combos
                List<ProductCombo> listProductCombo = new List<ProductCombo>();
                List<ProductComboResponseModel> listProductComboResponse = new List<ProductComboResponseModel>();
                combo.TotalValue = 0;

                foreach(var detail in request.ProductCombos)
                {
                    ProductCombo productCombo = new ProductCombo();
                    productCombo.ProductId = detail.ProductId;
                    productCombo.Quantity = detail.Quantity;
                    productCombo.ComboId = combo.Id;
                    listProductCombo.Add(productCombo);

                    var productComboResult = _mapper.Map<ProductCombo, ProductComboResponseModel>(productCombo);
                    listProductComboResponse.Add(productComboResult);

                    //add totalvalue for combo
                    var productDetail = _unitOfWork.Repository<Product>().Find(x => x.Id.Equals(detail.ProductId));
                    if(productDetail == null)
                    {
                        throw new Exception("Not Found");
                    }
                    combo.TotalValue = combo.TotalValue + (productDetail.Price * detail.Quantity);
                }

                #endregion
                combo.ProductCombos = listProductCombo;

                await _unitOfWork.Repository<Combo>().InsertAsync(combo);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Combo, ComboResponseModel>(combo);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                return _mapper.Map<Combo, ComboResponseModel>(combo);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ComboResponseModel> GetComboByGuid(Guid Id)
        {
            var combo = await _unitOfWork.Repository<Combo>().GetAll().Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
            if (combo is not null)
            {
                return _mapper.Map<Combo, ComboResponseModel>(combo);
            }
            else throw new Exception("Not found");
        }

        public async Task<IEnumerable<ComboResponseModel>> GetCombos()
        {
           var combos = _unitOfWork.Repository<Combo>().GetAll().Include(x => x.ProductCombos).ToList();
            if (combos.Count > 0)
            {
                combos = combos.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<Combo>, IEnumerable<ComboResponseModel>>(combos);
            }
            else throw new Exception("Not have any combo");

        }

        public async Task<ComboResponseModel> UpdateCombo(Guid Id, UpdateComboRequestModel request)
        {
            try
            {
                Combo combo = null;
                combo = _unitOfWork.Repository<Combo>().Find(x => x.Id.Equals(Id));
                if(combo == null)
                {
                    throw new NotFoundException("Combo is not exist!");
                }
                _mapper.Map<UpdateComboRequestModel, Combo>(request, combo);
                await _unitOfWork.Repository<Combo>().UpdateDetached(combo);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Combo, ComboResponseModel>(combo);

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
