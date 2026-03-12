using CoopApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoopApplication.Persistence.Context
{
    public class CoopDbContext(DbContextOptions<CoopDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<LoanTaken> LoanTaken { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("case_insensitive");
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        public static void SeedAdminUser(CoopDbContext context)
        {
            // Ensure admin role exists
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = new Role("Admin");
                context.Roles.Add(adminRole);
                context.SaveChanges();
            }

            // Ensure association exists
            var association = context.Associations.FirstOrDefault();
            if (association == null)
            {
                association = new Association("Onward", "Default Association");
                context.Associations.Add(association);
                context.SaveChanges();
            }

            // Check if admin user already exists
            var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@coop.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@coop.com",
                    Phone = "1234567890",
                    RoleId = adminRole.Id,
                    AssociationId = association.Id,
                    IsActive = true
                    // Set other required properties
                };
                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
