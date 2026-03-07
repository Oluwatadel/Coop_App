using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class LoanTakenConfiguration : IEntityTypeConfiguration<LoanTaken>
    {
        public void Configure(EntityTypeBuilder<LoanTaken> entity)
        {
            entity.ToTable("loan_taken");

            entity.HasKey(l => l.Id);

            entity.Property(a => a.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("id");

            entity.Property(l => l.UserId)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("user_id");

            entity.Property(l => l.LoanType)
                .HasColumnName("loan_type")
                .HasConversion(new JsonValueConverter<LoanType>())
                .HasColumnType("jsonb")
                .IsRequired()
                .HasColumnName("loan_type");

            entity.Property(l => l.PrincipalAmount)
                .IsRequired()
                .HasColumnName("principal_amount")
                .HasColumnType("decimal(18,5)");

            entity.Property(l => l.TotalRepaymentAmount)
                .IsRequired()
                .HasColumnName("total_repayment_amount")
                .HasColumnType("decimal(18,5)");

            entity.Property(l => l.BalanceRemaining)
                .IsRequired()
                .HasColumnName("balance_remaining")
                .HasColumnType("decimal(18,5)");

            entity.Property(l => l.Status)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<LoanStatus>())
                .HasColumnName("status");

            entity.Property(l => l.StartDate)
                .IsRequired()
                .HasColumnName("start_date")
                .HasColumnType("timestamp with time zone");

            entity.Property(l => l.EndDate)
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasColumnName("end_date");

            entity.Property(l => l.LoanRepayments)
                .HasConversion(new JsonValueConverter<HashSet<LoanRepayment>>())
                .HasColumnName("loan_repayments")
                .HasColumnType("jsonb");

            entity.Property(l => l.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasColumnName("created_date");
            
            entity.Property(l => l.ModifiedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired(false)
                .HasColumnName("modified_date");

            entity.Property(l => l.Modifier)
                .HasColumnType("uuid")
                .IsRequired(false)
                .HasColumnName("modifier_id");

            entity.HasIndex(l => l.Id)
                .IsUnique();

            entity.HasIndex(l => l.UserId)
                .IsUnique();

            entity.HasIndex(l => l.Status);
            entity.HasIndex(l => l.LoanType);
            entity.HasIndex(l => l.Modifier);
        }
    }
}
