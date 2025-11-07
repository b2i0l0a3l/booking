using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.presistence.config
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
            .HasOne<SalesInvoice>()
            .WithMany(i => i.Payments)
            .HasForeignKey("InvoiceId")
            .IsRequired(false) 
            .HasConstraintName("FK_Payment_SalesInvoice")
            .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}