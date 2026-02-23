using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.BookedAt);

            builder.OwnsOne(_ => _.Type);

            builder.Property<Guid>("EventId");

            builder.HasIndex("EventId");

            builder.Property<Guid?>("BookingId");

            builder.HasOne(x => x.Booking)
                   .WithMany(x => x.Tickets)
                   .HasForeignKey("BookingId")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property<byte[]>("Version")
                   .IsRowVersion()
                   .IsConcurrencyToken();
        }
    }
}
