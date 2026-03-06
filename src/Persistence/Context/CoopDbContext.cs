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
    }
}
