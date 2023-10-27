using AutoMapper;
using ComboService.Application.ViewModels.PublishedModels;
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
			CreateMap<ComboPublishedModel, ComboResponseModel>().ReverseMap();
			CreateMap< ComboResponseModel, Combo>()
                .ForPath(x=>x.Shop.ShopName,opt=>opt.MapFrom(x=>x.ShopName))
                .ForPath(x=>x.PriceLists,opt=>opt.MapFrom(x=>x.PriceList))
                .ReverseMap();

            CreateMap<Combo,CreateComboRequestModel>()
                .ForMember(x=>x.File,opt=>opt.Ignore())
                .ReverseMap();


            CreateMap<UpdateComboRequestModel, Combo>();
            #endregion

            #region PriceList
            CreateMap<PriceList, PriceListResponseModel>().ReverseMap();
            CreateMap<CreatePriceListRequestModel, PriceList>().ReverseMap();
            CreateMap<UpdatePriceListRequestModel, PriceList>().ReverseMap();
            #endregion

            #region ProductCombo
            CreateMap<ProductComboResponseModel,ProductCombo>()
                .ForPath(x=>x.Product.ProductName,opt=>opt.MapFrom(x=>x.ProductName))
				.ForPath(x => x.Product.ProductName, opt => opt.MapFrom(x => x.ProductName))
				.ForPath(x => x.Product.Description, opt => opt.MapFrom(x => x.Description))
				.ForPath(x => x.Product.Status, opt => opt.MapFrom(x => x.Status))
				.ForPath(x => x.Product.Size, opt => opt.MapFrom(x => x.Size))
				.ForPath(x => x.Product.Color, opt => opt.MapFrom(x => x.Color))
				.ForPath(x => x.Product.Material, opt => opt.MapFrom(x => x.Material))
				.ForPath(x => x.Product.Price, opt => opt.MapFrom(x => x.Price))
				.ForPath(x => x.Product.Compesation, opt => opt.MapFrom(x => x.Compesation))
				.ForPath(x => x.Product.CategoryName, opt => opt.MapFrom(x => x.CategoryName))
				.ForPath(x => x.Product.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
				.ForPath(x => x.Product.ProductImages, opt => opt.MapFrom(x => x.ProductImages))
				.ForPath(x => x.Combo.ShopId, opt => opt.MapFrom(x => x.ShopId))
				.ForPath(x => x.Combo.ComboName, opt => opt.MapFrom(x => x.ComboName))
				.ForPath(x => x.Combo.Shop.ShopName, opt => opt.MapFrom(x => x.ShopName))
				.ReverseMap();
            CreateMap<ProductComboRequestModel, ProductCombo>().ReverseMap();
			CreateMap<UpdateProductComboRequest, ProductCombo>().ReverseMap();
			#endregion

			#region Product
			CreateMap<Product, ProductResponseModel>().ReverseMap();
            CreateMap<Product,ProductCreateModel>()
                .ForMember(x=>x.ProductImages,opt=>opt.Ignore())
                .ReverseMap();
            #endregion

            #region Shop
            CreateMap<Shop, ShopResponseModel>().ReverseMap();
			CreateMap<Shop, ShopCreateModel>().ReverseMap();
			#endregion

            CreateMap<ProductImage,ImageReadModel>().ReverseMap();
		}
    }
}
