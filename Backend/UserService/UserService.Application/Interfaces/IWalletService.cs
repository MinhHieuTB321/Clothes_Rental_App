using UserService.Application.ViewModels.Wallets;

namespace UserService.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletReadModel> CreateWallet(WalletCreateModel model);
        Task<bool> UpdateWallet(WalletUpdateModel model);

        Task<WalletReadModel> GetWalletbyId(Guid id);
    }
}