using Microsoft.EntityFrameworkCore;

namespace ProjectARTEMIS.Infrastructure.Persistence
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
