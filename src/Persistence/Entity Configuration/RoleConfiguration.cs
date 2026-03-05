using CoopApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("role");

            entity.HasKey(r => r.Id);

            entity.Property(r => r.Name)
                .IsRequired()
                .HasColumnName("role_name")
                .HasColumnType("varchar(50)");

            entity.Property(r => r.CreatedAt)
                .HasColumnName("create_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(r => r.ModifiedAt)
                .HasColumnName("modified_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);

            entity.Property(r => r.Modifier)
                .HasColumnName("modified_by")
                .HasColumnType("uuid")
                .IsRequired(false);

            entity.HasIndex(r => r.Name);

        }
    }
}
