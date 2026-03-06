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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.ToTable("transaction");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(t => t.LoanId)
                .HasColumnName("loan_id");

            entity.Property(t => t.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.TransactionType)
                .IsRequired()
                .HasColumnName("transaction_type");

            entity.Property(t => t.PaymentMethod)
                .IsRequired()
                .HasColumnName("payment_method");

            entity.Property(t => t.AdminId)
                .IsRequired()
                .HasColumnName("admin_id");

            entity.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("date");

            entity.HasIndex(t => t.UserId);
            entity.HasIndex(t => t.LoanId);
        }
    }
}
