﻿using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures.FluentAPIs
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.Id).ValueGeneratedNever();
			builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);
		}
	}
}
