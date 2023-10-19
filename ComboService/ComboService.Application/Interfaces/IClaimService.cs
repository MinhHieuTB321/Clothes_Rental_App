using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IClaimService
    {
        public Guid GetCurrentUser {  get; }

        public string GetEmail { get; }
    }
}
