using System;
using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{   
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        // Indexes
        builder.HasIndex(t => t.UserId); 
        builder.HasIndex(t => new { t.UserId, t.CreatedAt });
        builder.HasIndex(t => t.IsDeleted); 

        // Required properties
        builder.Property(t => t.Amount)
            .HasPrecision(18, 2)
            .IsRequired();
        
        builder.Property(t => t.UserId).IsRequired();

        builder.Property(t => t.CreatedAt).IsRequired();

        builder.Property(t => t.UpdatedAt)
            .IsRequired(false);

        // Soft delete
        builder.Property(t => t.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(t => t.Description).HasMaxLength(255);

        // Relationship with Category
        builder.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
