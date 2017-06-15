using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DnD.Models.DatabaseContext
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(x => x.PasswordHash).HasColumnName("Password");
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.AccessFailedCount);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.ConcurrencyStamp);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.EmailConfirmed);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.LockoutEnabled);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.LockoutEnd);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.Logins);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.PhoneNumber);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.PhoneNumberConfirmed);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.SecurityStamp);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.TwoFactorEnabled);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.Email);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.NormalizedEmail);
            modelBuilder.Entity<IdentityUser>().Ignore(x => x.NormalizedUserName);
		}

    }
}
