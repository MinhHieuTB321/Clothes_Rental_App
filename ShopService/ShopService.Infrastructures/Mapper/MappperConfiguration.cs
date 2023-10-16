using AutoMapper;
using ShopService.Application.ViewModels.Categories;
using ShopService.Application.ViewModels.Images;
using ShopService.Application.ViewModels.Owners;
using ShopService.Application.ViewModels.Products;
using ShopService.Application.ViewModels.Shops;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.Mapper
{
    public class MappperConfiguration:Profile
    {
        public MappperConfiguration()
        {
            #region CATEGORY
            CreateMap<CategoryCreateModel, Category>().ReverseMap();
            CreateMap<CategoryReadModel, Category>().ReverseMap();
            CreateMap<CategoryUpdateModel, Category>().ReverseMap();
            #endregion
            
            #region  IMAGE
            CreateMap<ImageCreateModel, ProductImage>().ReverseMap();
            CreateMap<ImageReadModel, ProductImage>().ReverseMap();
            #endregion

            #region  Owner
            CreateMap<OwnerReadModel, OwnerPublishedModel>().ReverseMap();
            CreateMap<OwnerCreateModel, Owner>().ReverseMap();
            CreateMap<OwnerReadModel, Owner>().ReverseMap();
            CreateMap<OwnerUpdateModel, Owner>().ReverseMap();
            #endregion

            #region PRODUCT
            CreateMap<ProductCreateModel, Product>().ReverseMap();
            CreateMap<ProductReadModel, Product>()
                .ForPath(x=>x.Shop.ShopName,opt=>opt.MapFrom(x=>x.ShopName))
                .ForPath(x => x.Category.CategoryName, opt => opt.MapFrom(x => x.CategoryName))
                .ReverseMap();
            CreateMap<ProductUpdateModel, Product>().ReverseMap();
            CreateMap<ProductReadModel, ProductPublishedModel>().ReverseMap();
            #endregion

            #region SHOP
            CreateMap<ShopReadModel, ShopPublishedModel>().ReverseMap();
            CreateMap<ShopCreateModel, Shop>().ReverseMap();
            CreateMap<ShopReadModel, Shop>()
                .ForPath(x=>x.Owner.Name,opt=>opt.MapFrom(x=>x.OwnerName))
                .ReverseMap();
            CreateMap<ShopUpdateModel, Shop>().ReverseMap();

            #endregion
        }
    }
}
