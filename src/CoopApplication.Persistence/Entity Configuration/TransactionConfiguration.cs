using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.ToTable("transaction");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .HasColumnType("uuid")
                .HasColumnName("id")
                .IsRequired();

            entity.Property(t => t.TransactionReferenceNo)
                .HasColumnName("transaction_reference_no")
                .HasColumnType("varchar(20)")
                .IsRequired();

            entity.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(t => t.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.TransactionType)
                .IsRequired()
                .HasColumnName("transaction_type")
                .HasConversion(new EnumToStringConverter<TransactionType>());

            entity.Property(t => t.PaymentMethod)
                .IsRequired()
                .HasColumnName("payment_method")
                .HasConversion(new EnumToStringConverter<PaymentMethod>());

            entity.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("date");

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

            entity.HasIndex(t => t.UserId);
        }
    }
}
