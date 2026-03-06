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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("user");
            entity.HasKey(u => u.Id);
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

            entity.HasOne(u => u.Association)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AssociationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Role)
                .WithMany()
                //.WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(u => u.Email).IsUnique();
        }
    }
}
