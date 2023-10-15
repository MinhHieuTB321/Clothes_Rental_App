using AutoMapper;
using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.OrderDetails;
using OrderService.Application.ViewModels.Orders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructures.Mappers
{
    public class MapperConfiguration:Profile
    {
        public MapperConfiguration()
        {
            //Src => Dest
            #region CUSTOMER
            CreateMap<Customer,CustomerReadModel>().ReverseMap();
            CreateMap<CustomerCreateModel, Customer>().ReverseMap();
            CreateMap<CustomerUpdateModel, Customer>().ReverseMap();
            CreateMap<CustomerReadModel, CustomerPublishedModel>();
            #endregion

            #region ORDER

            CreateMap<OrderCreateModel,Order>()
                .ForMember(x=>x.OrderDetails,opt=>opt.Ignore())
                .ReverseMap();
            CreateMap<OrderUpdateModel, Order>()
               .ReverseMap();
            CreateMap< OrderReadModel, Order>()
                .ForPath(x=>x.Customer.Name,opt=>opt.MapFrom(x=>x.CustomerName))
                .ForPath(x => x.Customer.Address, opt => opt.MapFrom(x => x.CustomerAddress))
                .ForPath(x => x.Customer.Phone, opt => opt.MapFrom(x => x.CustomerPhone))
                .ForPath(x => x.Shop!.ShopName, opt => opt.MapFrom(x => x.ShopName))
                .ForPath(x => x.Shop!.ShopEmail, opt => opt.MapFrom(x => x.ShopEmail))
                .ForPath(x => x.Shop!.Address, opt => opt.MapFrom(x => x.ShopAddress))
                .ForPath(x => x.Shop!.ShopPhone, opt => opt.MapFrom(x => x.ShopPhone))
                .ForPath(x => x.Shop!.OwnerId, opt => opt.MapFrom(x => x.OwnerId))
                .ForMember(x=>x.OrderDetails,opt=>opt.Ignore())
                .ReverseMap();
            CreateMap<OrderReadModel,OrderPublishedModel>().ReverseMap();

            #endregion

            #region ORDER-DETAIL
            CreateMap<OrderDetailReadModel,OrderDetail>()
                .ForPath(x => x.Combo.ComboName, opt => opt.MapFrom(x => x.ComboName))
                .ReverseMap();
            CreateMap<OrderDetailCreateModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailFirebase, OrderDetail>()
                .ReverseMap();
            #endregion
        }
    }
}
