using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechNest.Domain.Entities;

namespace TechNest.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.FullName).IsRequired().HasMaxLength(150);
        builder.Property(o => o.Email).IsRequired().HasMaxLength(150);
        builder.Property(o => o.Phone).IsRequired().HasMaxLength(50);
        builder.Property(o => o.City).IsRequired().HasMaxLength(100);
        builder.Property(o => o.Department).IsRequired().HasMaxLength(150);
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(o => o.CreatedAt).IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}