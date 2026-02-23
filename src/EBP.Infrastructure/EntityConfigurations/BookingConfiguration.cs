using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property<Guid>("EventId");

            builder.HasIndex("EventId");
        }
    }
}
