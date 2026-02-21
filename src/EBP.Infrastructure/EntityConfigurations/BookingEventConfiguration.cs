using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public class BookingEventConfiguration : IEntityTypeConfiguration<BookingEvent>
    {
        public void Configure(EntityTypeBuilder<BookingEvent> builder)
        {
            builder.ToTable("BookingEvents");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .IsRequired();
            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.Property(x => x.Desciption)
                   .HasMaxLength(1000);

            builder.Property(x => x.StartAt)
                   .IsRequired();

            builder.Property(x => x.Duration)
                   .IsRequired()
                   .HasConversion(v => v, v => v);
        }
    }
}
