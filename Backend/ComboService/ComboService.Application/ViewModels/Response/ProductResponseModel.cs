﻿using ComboService.Application.ViewModels.PublishedModels;
using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Response
{
    public class ProductResponseModel
    {
		 public Guid Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public int Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; } = default!;
        public double Price { get; set; } = default!;
        public double Compesation { get; set; } = default!;
        public Guid? RootProductId { get; set; } = default!;

        public Guid ShopId { get; set; } = default!;
        public string ShopName {  get; set; } = default!;

        public Guid CategoryId { get; set; } = default!;
        public string CategoryName { get; set; } = default!;

        public ICollection<ProductResponseModel> SubProducts { get; set; }=default!;
        public ICollection<ImageReadModel> ProductImages { get; set; } = default!;

    }
}
