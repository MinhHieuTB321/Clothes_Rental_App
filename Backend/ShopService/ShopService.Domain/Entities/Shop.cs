﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain.Entities
{
    public class Shop : BaseEntity
    {
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = Guid.NewGuid().ToString();
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string FileName { get; set; } = "ShopLogo";
        public string FileUrl { get; set; } = default!;
        public string Status { get; set; } = default!;

        public Guid OwnerId { get; set; } = default!;
        public Owner Owner { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }


    }
}
