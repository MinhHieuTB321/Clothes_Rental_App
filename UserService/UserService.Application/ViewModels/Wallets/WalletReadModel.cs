using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Transactions;

namespace UserService.Application.ViewModels.Wallets
{
    public class WalletReadModel
    {
        public Guid Id {  get; set; }
        public double Balance { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public List<TransactionReadModel>? Transactions { get; set; }
    }
}
