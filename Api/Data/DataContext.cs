using System;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property<int>(u => u.Id)
                                       .HasColumnName("id");

            modelBuilder.Entity<User>().HasIndex(u => u.Name)
                                       .IsUnique()
                                       .HasDatabaseName("NameIsUnique");
                                                                              
            modelBuilder.Entity<User>().Property<string>(u => u.Name)
                                       .HasColumnName("name_view")
                                       .HasMaxLength(100)
                                       .IsRequired();

            modelBuilder.Entity<User>().HasIndex(u => u.Email)
                                       .IsUnique()
                                       .HasDatabaseName("EmailIsUnique");
                                                                              
            modelBuilder.Entity<User>().Property<string>(u => u.Email)
                                       .HasColumnName("email")
                                       .HasMaxLength(100)
                                       .IsRequired();

            modelBuilder.Entity<User>().Property<string>(u => u.Password)
                                       .HasColumnName("password")
                                       .HasMaxLength(200)
                                       .IsRequired();
            
            modelBuilder.Entity<User>().Property<DateTime>(u => u.CreatedAt)
                                       .HasColumnName("dt_created");                                                                          

            modelBuilder.Entity<User>().Property<DateTime>(u => u.UpdatedAt)
                                       .HasColumnName("dt_update")
                                       .HasDefaultValueSql("GETDATE()");                                       

        }

    }
}