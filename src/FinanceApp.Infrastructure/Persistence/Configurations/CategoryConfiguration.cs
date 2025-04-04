using System;
using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.UserId); 
        builder.HasIndex(c => new { c.UserId, c.Type });


        // Required properties
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        // Set dates
        builder.Property(c => c.CreatedAt).IsRequired();
        
        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);       
    }
}
