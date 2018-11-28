using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Relational;
using Snowclone.Data;

namespace Snowclone.Entities
{
    public class TenantDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.HasIndex(e => e.UserId).IsUnique();

                entity.HasIndex(e => e.Username).IsUnique();

                entity.HasIndex(e => new { e.MemberId, e.Email });

                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.MemberId).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).IsRequired().HasColumnType("varchar(128)").HasMaxLength(30);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(30);

                //entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.Username).IsRequired().HasMaxLength(30);

                entity.Property(e => e.Handle).IsRequired().HasMaxLength(30);

                entity.HasMany(e => e.Messages).WithOne(d => d.Member).HasForeignKey(e => e.MemberId);
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasKey(e => e.ChannelId);

                entity.HasAlternateKey(e => e.ChannelName);

                entity.Property(e => e.ChannelId).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.ChannelName).IsUnique();

                entity.HasIndex(e => new { e.ChannelId, e.ChannelName });

                entity.Property(e => e.ChannelName).IsRequired().HasMaxLength(40);

                entity.Property(e => e.isPrivate).IsRequired();

                entity.HasMany(e => e.Messages).WithOne(m => m.Channel).HasForeignKey(e => e.ChannelId).HasPrincipalKey(e => e.ChannelName);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).ValueGeneratedOnAdd();

                entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);

                entity.HasOne(e => e.Member).WithMany(m => m.Messages).HasForeignKey(e => e.MemberId).IsRequired();

                entity.HasOne(e => e.Channel).WithMany(c => c.Messages).HasForeignKey(e => e.ChannelId).IsRequired();
            });
        }
    }
}
