﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class ProductCombo : BaseEntity
    {
        public int Quantity { get; set; } = default!;

        //Combo
        public Guid ComboId { get; set; } = default!;

        public Combo Combo { get; set; } = default!;

        //Product
        public Guid ProductId { get; set; } = default!;

        public Product Product { get; set; } = default!;
    }
}
