using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using ChatApi.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.presistence.config
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.HasOne(x => x.Service).WithMany(x => x.bookings)
            .HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<ApplicationUser>(x=>(ApplicationUser?)x.user).WithMany(x=>x.bookings).HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}