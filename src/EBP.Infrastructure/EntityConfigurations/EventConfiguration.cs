using EBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBP.Infrastructure.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .IsRequired();
            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.Property(x => x.Desciption);

            builder.Property(x => x.StartAt)
                   .IsRequired();

            builder.Property(x => x.Duration)
                   .IsRequired();

            builder.HasMany(x => x.Tickets)
                   .WithOne(x => x.Event)
                   .HasForeignKey("EventId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Tickets)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Bookings)
                   .WithOne(x => x.Event)
                   .HasForeignKey("EventId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Bookings)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
