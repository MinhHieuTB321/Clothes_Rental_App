using AutoMapper;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Combo
            CreateMap<Combo, ComboResponseModel>().ReverseMap();
            CreateMap<CreateComboRequestModel, Combo>();
            CreateMap<UpdateComboRequestModel, Combo>();
            #endregion

            #region PriceList
            CreateMap<PriceList, PriceListResponseModel>().ReverseMap();
            CreateMap<CreatePriceListRequestModel, PriceList>();
            CreateMap<UpdatePriceListRequestModel, PriceList>();
            #endregion

            #region ProductCombo
            CreateMap<ProductCombo, ProductComboResponseModel>().ReverseMap();
            CreateMap<ProductComboRequestModel, ProductCombo>();
            #endregion

            #region Product
            CreateMap<Product, ProductResponseModel>().ReverseMap();
            #endregion

            #region Shop
            CreateMap<Shop, ShopResponseModel>().ReverseMap();
            #endregion
        }
    }
}
