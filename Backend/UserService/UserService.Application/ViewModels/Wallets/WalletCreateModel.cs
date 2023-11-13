using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ViewModels.Wallets
{
    public class WalletCreateModel
    {
        public double Balance { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }
    }
}
