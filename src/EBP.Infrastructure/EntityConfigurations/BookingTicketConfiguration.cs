using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public sealed class BookingTicketConfiguration : IEntityTypeConfiguration<BookingTicket>
    {
        public void Configure(EntityTypeBuilder<BookingTicket> builder)
        {
            builder.ToTable("BookingTickets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property<Guid>("BookingEventId");

            builder.HasIndex("BookingEventId");

            builder.Property<byte[]>("Version")
                   .IsRowVersion()
                   .IsConcurrencyToken();
        }
    }
}
