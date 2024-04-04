using Microsoft.EntityFrameworkCore;

namespace Login_WPF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=loginWPF;Trusted_Connection=True;");
        }
    }
}
