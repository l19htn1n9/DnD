using System;
using DnD.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace DnD.Models.DatabaseContext
{
    public class DndEntities : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }

        public DndEntities()
        {
            Database.EnsureCreated();
        }

        public DndEntities(DbContextOptions<DndEntities> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=localhost;Database=DnD;Uid=DnD;Pwd=Admin123;SslMode=None";
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}
