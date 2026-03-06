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
    public class LoanTypeConfiguration : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> entity)
        {
            entity.ToTable("loan_type");

            entity.HasKey(l => l.Id);

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

            entity.Property(l => l.LiquidityPeriod)
                .IsRequired()
                .HasColumnName("liquidity_period");

            entity.HasMany(l => l.Loans)
                 .WithOne(lt => lt.LoanType)
                 .HasForeignKey(lt => lt.LoanTypeId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(l => l.Name).IsUnique();
        }
    }
}




