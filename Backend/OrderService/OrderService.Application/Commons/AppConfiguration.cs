using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commons
{
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; } = default!;
        public string JWTSecretKey { get; set; } = default!;
        public string ProjectId { get; set; } = default!;
        public string PrivateKeyId { get; set; } = default!;
        public string PrivateKey { get; set; } = default!;
        public string ClientEmail { get; set; } = default!;
    }
}
