using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Models;
using UsersAPI.Infra.Data.Configurations;

namespace UsersAPI.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        
        public DbSet<User> Users { get; set; }
    }
}
