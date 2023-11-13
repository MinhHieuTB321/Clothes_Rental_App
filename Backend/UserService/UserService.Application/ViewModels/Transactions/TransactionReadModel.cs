using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.ViewModels.Transactions
{
    public class TransactionReadModel
    {
        public Guid Id {  get; set; }  
        public double Amount { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid PaymentId { get; set; }
        public Guid WalletId { get; set; }
    }
}
