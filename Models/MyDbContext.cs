
using System;
using DeveloperNotebookAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DeveloperNotebookAPI.Models
{
    public class MyDbContext: DbContext
    {
        public DbSet<Category> Categories {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Platform> Platforms {get; set;}
        public DbSet<Command> Commands {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            IConfigurationRoot vConfiguration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddEnvironmentVariables()
                .Build();

            var vDBServer = vConfiguration["DBServer"] ?? "localhost";
            var vDBPort = vConfiguration["DBPort"] ?? "1433";
            var vDBName = vConfiguration["DBName"] ?? "DEVNOTE";
            var vDBUser = vConfiguration["DBUser"] ?? "sa";
            var vDBPassword = vConfiguration["DBPassword"] ?? "Testing1122";

            var vDBConnString = $"Server={vDBServer},{vDBPort};Initial Catalog={vDBName};User ID={vDBUser};Password={vDBPassword}";

            optionsBuilder.UseSqlServer(vDBConnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => new { u.Id })
                .HasName("PK_Users");
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Username })
                .IsUnique()
                .HasName("UN_User");

            modelBuilder.Entity<Category>()
                .HasKey(c => new { c.Id })
                .HasName("PK_Categories");
            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.Name, c.ActiveRecord })
                .IsUnique()
                .HasName("UN_Category");
        }
    }
}