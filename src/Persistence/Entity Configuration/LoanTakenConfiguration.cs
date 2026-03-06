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
    public class LoanTakenConfiguration : IEntityTypeConfiguration<LoanTaken>
    {
        public void Configure(EntityTypeBuilder<LoanTaken> entity)
        {
            entity.ToTable("loan_taken");

            entity.HasKey(l => l.Id);

            entity.Property(l => l.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(l => l.LoanTypeId)
                .IsRequired()
                .HasColumnName("loan_type_id");

            entity.Property(l => l.PrincipalAmount)
                .IsRequired()
                .HasColumnName("principal_amount")
                .HasColumnType("decimal(18,2)");

            entity.Property(l => l.TotalPayable)
                .IsRequired()
                .HasColumnName("total_payable")
                .HasColumnType("decimal(18,2)");

            entity.Property(l => l.BalanceRemaining)
                .IsRequired()
                .HasColumnName("balance_remaining")
                .HasColumnType("decimal(18,2)");

            entity.Property(l => l.Status)
                .IsRequired()
                .HasColumnName("status");

            entity.Property(l => l.StartDate)
                .IsRequired()
                .HasColumnName("start_date");

            entity.Property(l => l.EndDate)
                .IsRequired()
                .HasColumnName("end_date");
        }
    }
}
