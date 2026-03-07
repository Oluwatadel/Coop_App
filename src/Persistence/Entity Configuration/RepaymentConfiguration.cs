using CoopApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class LoanRepaymentConfiguration : IEntityTypeConfiguration<LoanRepayment>
    {
        public void Configure(EntityTypeBuilder<LoanRepayment> builder)
        {
            builder.ToTable("loanRepayment");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("uuid");

            builder.Property(l => l.LoanId)
                .IsRequired()
                .HasColumnName("loan_id")
                .HasColumnType("uuid");
            
            builder.Property(l => l.TransactionId)
                .IsRequired()
                .HasColumnName("transaction_id")
                .HasColumnType("uuid");

            builder.Property(l => l.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,5)")
                .HasColumnName("loan_repayment_amount");

            builder.Property(l => l.Date)
                .HasColumnName("payment_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .HasColumnName("created_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(l => l.CreatedBy)
                .HasColumnName("creator_id")
                .HasColumnType("uuid")
                .IsRequired();



            builder.HasIndex(l => l.Id)
                .IsUnique();
            builder.HasIndex(l => l.TransactionId)
                .IsUnique();
            builder.HasIndex(l => l.LoanId)
                .IsUnique();
            builder.HasIndex(l => l.Date);
            builder.HasIndex(l => l.CreatedBy)
                .IsUnique();

        }
    }
}
