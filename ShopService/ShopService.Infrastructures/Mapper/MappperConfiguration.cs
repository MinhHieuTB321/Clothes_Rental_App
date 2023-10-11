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
            CreateMap<CategoryCreateModel, Category>().ReverseMap();
            CreateMap<CategoryReadModel, Category>().ReverseMap();
            CreateMap<CategoryUpdateModel, Category>().ReverseMap();

            CreateMap<ImageCreateModel, ProductImage>().ReverseMap();
            CreateMap<ImageReadModel, ProductImage>().ReverseMap();

            CreateMap<OwnerCreateModel, Owner>().ReverseMap();
            CreateMap<OwnerReadModel, Owner>().ReverseMap();
            CreateMap<OwnerUpdateModel, Owner>().ReverseMap();

            CreateMap<ProductCreateModel, Product>().ReverseMap();
            CreateMap<ProductReadModel, Product>().ReverseMap();
            CreateMap<ProductUpdateModel, Product>().ReverseMap();

            CreateMap<ShopCreateModel, Shop>().ReverseMap();
            CreateMap<ShopReadModel, Shop>().ReverseMap();
            CreateMap<ShopUpdateModel, Shop>().ReverseMap();
        }
    }
}
