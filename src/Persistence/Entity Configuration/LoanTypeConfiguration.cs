using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class LoanTypeConfiguration : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> entity)
        {
            entity.ToTable("loan_type");

            entity.HasKey(l => l.Id);

            entity.Property(l => l.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("uuid");

            entity.Property(l => l.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

            entity.Property(l => l.MinimumLoanRepayment)
                .IsRequired()
                .HasColumnName("minimum_loan_repayment");

            entity.Property(l => l.AnnualInterestRate)
                .IsRequired()
                .HasColumnName("annual_interest_rate")
                .HasColumnType("decimal(5,2)");

            entity.Property(l => l.LiquidityPeriodInMonths)
                .IsRequired()
                .HasColumnName("liquidity_period");

            entity.Property(l => l.LoanVersion)
                .IsRequired()
                .HasColumnName("loan_version")
                .HasColumnType("varchar(50)");
            
            entity.Property(l => l.Description)
                .IsRequired(false)
                .HasColumnName("loan_description")
                .HasColumnType("text");

            entity.Property(l => l.PreviousLoanVersionId)
                .HasColumnName("previous_loan_type")
                .HasColumnType("uuid")
                .IsRequired(false);

            entity.Property(l => l.MinimumLoanAmount)
                .HasColumnName("min_loan_amount")
                .HasColumnType("decimal(18,5)")
                .IsRequired();
            
            entity.Property(l => l.MaximumLoanAmount)
                .HasColumnName("max_loan_amount")
                .HasColumnType("decimal(18,5)")
                .IsRequired();

            entity.HasIndex(l => l.Name);
            entity.HasIndex(l => l.Id).IsUnique();
        }
    }
}




