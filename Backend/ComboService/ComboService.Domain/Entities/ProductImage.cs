﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
	public class ProductImage:BaseEntity
	{
		public string FileName { get; set; } = default!;
		public string FileUrl { get; set; } = default!;

		public Guid ProductId { get; set; }

		public Product Product { get; set; } = default!;
	}
}
