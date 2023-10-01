using AutoMapper;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Transactions;
using UserService.Application.ViewModels.Users;
using UserService.Application.ViewModels.Wallets;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Mappers
{
    public class MapperConfigurationsProfile:Profile
    {
        public MapperConfigurationsProfile()
        {
            //Src=> Dest
            #region PAYMENT
                CreateMap<Payment,PaymentCreateModel>()
                .ReverseMap();

                CreateMap<PaymentReadModel, Payment>()
                //.ForPath(x => x.User!.Phone, opt => opt.MapFrom(x => x.Phone))
                //.ForPath(x => x.User!.Name, opt => opt.MapFrom(x => x.PartyName))
                .ReverseMap();

                CreateMap<TransactionCreateModel, Payment>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.PartyId))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Amount))
                .ForMember(x => x.Method, opt => opt.MapFrom(x => x.Method))
                .ReverseMap(); 


            #endregion

            #region TRANSACTION
            CreateMap<TransactionCreateModel,Transaction>().ReverseMap();
            CreateMap<Transaction, TransactionReadModel>().ReverseMap();
            #endregion

            #region WALLET
            CreateMap<WalletCreateModel, Wallet>().ReverseMap();
            CreateMap<Wallet, WalletReadModel>()
                .ForMember(x => x.Transactions, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<WalletUpdateModel,Wallet>().ReverseMap();
            #endregion

            #region USER
            CreateMap<User, UserReadModel>()
                .ForMember(x => x.WalletId, opt => opt.Ignore())
                .ForMember(x => x.Balance, opt => opt.Ignore())
                .ForMember(x => x.Payments, opt => opt.Ignore())
                .ReverseMap();
            #endregion  
        }
    }
}