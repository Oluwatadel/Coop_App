using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CoopApplication.Persistence.Entity_Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("user");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid");
            
            entity.Property(u => u.AssociationId)
                .HasColumnName("association_id")
                .HasColumnType("uuid")
                .IsRequired();
            
            entity.Property(u => u.RoleId)
                .HasColumnName("role_id")
                .HasColumnType("uuid")
                .IsRequired();

            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasColumnName("first_name")
                .HasColumnType("varchar(50)");

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasColumnType("varchar(50)");

            entity.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(100)");

            entity.Property(u => u.Phone)
                .IsRequired()
                .HasColumnName("phone")
                .HasColumnType("varchar(20)");

            entity.Property(u => u.IsActive)
                .IsRequired()
                .HasColumnName("is_active")
                .HasColumnType("boolean");

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

            entity.Property(u => u.TransactionIds)
                .HasColumnName("transaction_id")
                .HasConversion(new JsonValueConverter<ICollection<Guid>>());
            
            entity.Property(u => u.LoansTakenIds)
                .HasColumnName("loan_taken_id")
                .HasConversion(new JsonValueConverter<ICollection<Guid>>());



            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.AssociationId);
            entity.HasIndex(u => u.RoleId);
            entity.HasIndex(u => u.Phone).IsUnique();
        }
    }
}
