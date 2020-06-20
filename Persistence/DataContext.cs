namespace Persistence
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(x => x.HasKey(c => c.Id));
            modelBuilder.Entity<Order>(x => x.HasKey(o => o.Id));

            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Customer);
        }
    }
}
