using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public class BookingRefundConfiguration : IEntityTypeConfiguration<BookingRefund>
    {
        public void Configure(EntityTypeBuilder<BookingRefund> builder)
        {
            builder.ToTable("BookingRefunds");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.IsRefunded);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property<Guid>("BookingId");

            builder.HasIndex("BookingId")
                   .IsUnique();

            builder.HasOne(x => x.Booking)
                   .WithOne()
                   .HasForeignKey<BookingRefund>("BookingId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
