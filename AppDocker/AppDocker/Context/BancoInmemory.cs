using AppDocker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocker.Context
{
    public class BancoInmemory : DbContext
    {
        public DbSet<Usuarios> Usuario { get; set; }
        public BancoInmemory(DbContextOptions options) : base(options) { }

        public BancoInmemory() => this.Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase("Banco");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().HasKey(c => c.Id);
            modelBuilder.Entity<Phones>().HasNoKey();
            modelBuilder.Entity<Usuarios>().HasData(new Usuarios()

            {
                Id = Guid.NewGuid(),
                Name = "jose ",
                Email = "jose_m32@gmail.com",
                Password = "123",
                Phones = new List<Phones>()
                {
                   new Phones()
                   {
                       
                       Number ="123",
                       Ddd="123"
                   }
                }
            }); ; 
            
           
        }

       
    }

    
}
