using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.presistence.config
{
    public class ProductConfiugration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
            .HasIndex(p => p.SKU)
            .IsUnique();
            
            builder
            .HasIndex(p => p.Barcode)
            .IsUnique();
        }
    }
}