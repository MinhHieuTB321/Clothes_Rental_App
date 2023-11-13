using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.GlobalExceptionHandling
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string? message) : base(message)
        {
            
        }
    }
}
