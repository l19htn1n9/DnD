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

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
            //builder.HasDefaultSchema("Users");
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityUser>(p => 
            {
                p.Property(x => x.PasswordHash).HasColumnName("Password");
                p.Property(x => x.UserName).HasColumnName("Username");
                p.Property(x => x.NormalizedUserName).HasColumnName("Username");
                p.Property(x => x.SecurityStamp).HasColumnName("SecurityStamp");
                p.Ignore(x => x.AccessFailedCount);
                p.Ignore(x => x.ConcurrencyStamp);
                p.Ignore(x => x.EmailConfirmed);
                p.Ignore(x => x.LockoutEnabled);
                p.Ignore(x => x.LockoutEnd);
                p.Ignore(x => x.Logins);
                p.Ignore(x => x.PhoneNumber);
                p.Ignore(x => x.PhoneNumberConfirmed);
                p.Ignore(x => x.TwoFactorEnabled);
                p.Ignore(x => x.Email);
                p.Ignore(x => x.NormalizedEmail);
            });
            //builder.Entity<IdentityUser>().ToTable("Users").Property(x => x.PasswordHash).HasColumnName("Password");
            //builder.Entity<IdentityUser>().ToTable("Users").Property(x => x.UserName).HasColumnName("Username");
            //builder.Entity<IdentityUser>().ToTable("Users").Property(x => x.NormalizedUserName).HasColumnName("Username");
            //builder.Entity<IdentityUser>().ToTable("Users").Property(x => x.SecurityStamp).HasColumnName("SecurityStamp");
            //builder.Entity<IdentityUser>().Ignore(x => x.AccessFailedCount);
            //builder.Entity<IdentityUser>().Ignore(x => x.ConcurrencyStamp);
            //builder.Entity<IdentityUser>().Ignore(x => x.EmailConfirmed);
            //builder.Entity<IdentityUser>().Ignore(x => x.LockoutEnabled);
            //builder.Entity<IdentityUser>().Ignore(x => x.LockoutEnd);
            //builder.Entity<IdentityUser>().Ignore(x => x.Logins);
            //builder.Entity<IdentityUser>().Ignore(x => x.PhoneNumber);
            //builder.Entity<IdentityUser>().Ignore(x => x.PhoneNumberConfirmed);
            ////modelBuilder.Entity<IdentityUser>().Ignore(x => x.SecurityStamp);
            //builder.Entity<IdentityUser>().Ignore(x => x.TwoFactorEnabled);
            //builder.Entity<IdentityUser>().Ignore(x => x.Email);
            //builder.Entity<IdentityUser>().Ignore(x => x.NormalizedEmail);
            ////modelBuilder.Entity<IdentityUser>().Ignore(x => x.NormalizedUserName);
		}

    }
}
