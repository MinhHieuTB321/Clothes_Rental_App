﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = default!;
        public Guid RootProductId { get; set; } = default!;
        public Product RootProduct { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Compesation { get; set; } = default!;


        //Image
        [NotMapped]
        public ICollection<string> Images { get; set; } = default!;

        //Category
        public Guid CategoryId { get; set; } = default!;

    }
}
