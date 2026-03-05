using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Context
{
    public class CoopDbContext(DbContextOptions<CoopDbContext> options) : DbContext(options)
    {
    }
}
