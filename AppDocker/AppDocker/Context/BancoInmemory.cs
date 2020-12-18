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
        public DbSet<Phones> Phone { get; set; }
        public BancoInmemory(DbContextOptions options) : base(options) { }

        public BancoInmemory() => this.Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Banco");
        }
        public string Data = string.Format("{0:d/MM/yyyy HH:mm:ss}", DateTime.UtcNow);
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuarios>().HasKey(x => x.IdUser);
            modelBuilder.Entity<Phones>().HasKey(x => x.IdPhone);
            modelBuilder.Entity<Usuarios>().HasMany(b => b.Phones).WithOne(b => b.User);


            modelBuilder.Entity<Usuarios>().HasData(new Usuarios
            {
                IdUser = Guid.NewGuid(),
                Name = "admin",
                Email = "admin@com.br",
                Password = "admin",
                Created = Data,
                Last_login = Data,
                
            }
            );

        }

    }
}




