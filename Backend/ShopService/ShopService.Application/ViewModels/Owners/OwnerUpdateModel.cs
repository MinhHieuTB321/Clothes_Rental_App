﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Owners
{
    public class OwnerUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Status { get; set; } = default!;

    }
}
