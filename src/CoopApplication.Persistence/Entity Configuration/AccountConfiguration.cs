using CoopApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.ToTable("account");

            entity.HasKey(a => a.Id);

            entity.Property(a => a.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(a => a.TotalShares)
                .IsRequired()
                .HasColumnName("total_shares")
                .HasColumnType("decimal(18,2)");

            entity.Property(a => a.SavingsBalance)
                .IsRequired()
                .HasColumnName("savings_balance")
                .HasColumnType("decimal(18,2)");

            entity.Property(a => a.TotalInterestAccrued)
                .IsRequired()
                .HasColumnName("total_interest_accrued")
                .HasColumnType("decimal(18,2)");

            entity.Property(r => r.CreatedAt)
                .HasColumnName("create_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(r => r.ModifiedAt)
                .HasColumnName("modified_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);

            entity.Property(r => r.Modifier)
                .HasColumnName("modified_by")
                .HasColumnType("uuid")
                .IsRequired(false);

            entity.HasIndex(a => a.UserId).IsUnique();
            entity.HasIndex(a => a.TotalShares);
        }
    }
}
