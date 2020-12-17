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
            //optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase("Banco");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuarios>().HasKey(x => x.IdUser);
            modelBuilder.Entity<Phones>().HasKey(x => x.IdPhone);


            modelBuilder.Entity<Usuarios>()
            .HasMany(b => b.phones)
            .WithOne(b => b.User);



            //    List<Phones> ListPhones = new List<Phones>
            //    {
            //        new Phones { Number = "1111111111", DDD = "111" },
            //        new Phones { Number = "1111222211", DDD = "1211"}
            //    };
            //    modelBuilder.Entity<Phones>().HasData(ListPhones);



            //    modelBuilder.Entity<Usuarios>().HasData(new Usuarios
            //    {
            //        IdUser = Guid.NewGuid(),
            //        Name = "jose",
            //        Email = "jose23@gmail.com",
            //        Password = "js23",
            //        Phones = ListPhones,



            //    }) ;


        }

    }
}




