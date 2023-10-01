using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ViewModels.Payments;

namespace UserService.Application.ViewModels.Users
{
    public class UserReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Role { get; set; } = default!;
        public Guid WalletId { get; set; }
        public double Balance {  get; set; }
        public List<PaymentReadModel>? Payments { get; set; }
    }
}
