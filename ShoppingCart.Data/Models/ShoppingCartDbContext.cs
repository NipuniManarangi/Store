using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingCart.Data.Models
{
    public partial class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext()
        {
        }

        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder != null) { 
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-EN10G1A; Database=OnlineShopDB; Trusted_Connection=True; MultipleActiveResultSets=True;");
            }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Product>(entity =>
                {
                    entity.Property(e => e.Code).HasMaxLength(10);

                    entity.Property(e => e.UnitPrice).HasColumnType("money");
                });

                OnModelCreatingPartial(modelBuilder);
                modelBuilder.Entity<User>()
                    .HasKey(u => u.Email);
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
