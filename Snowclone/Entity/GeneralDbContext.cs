using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Snowclone.Entities
{
    public class GeneralDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenantUser>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.UserId });

                entity.HasOne(e => e.Tenant).WithMany(t => t.TenantUsers).HasForeignKey(e => e.TenantId);

                entity.HasOne(e => e.User).WithMany(u => u.TenantUsers).HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => new { e.UserId, e.Username });

                entity.HasIndex(e => e.Username).IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.Username).IsRequired().HasMaxLength(30);

                entity.Property(e => e.Email).IsRequired().HasMaxLength(128);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(30);
               
            });
        }
    }
}
