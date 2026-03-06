using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .HasColumnName("total_shares");

            entity.Property(a => a.SavingsBalance)
                .IsRequired()
                .HasColumnName("savings_balance")
                .HasColumnType("decimal(18,2)");

            entity.Property(a => a.TotalInterestAccrued)
                .IsRequired()
                .HasColumnName("total_interest_accrued")
                .HasColumnType("decimal(18,2)");

            entity.HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId);

            entity.HasIndex(a => a.UserId).IsUnique();
        }
    }
}
