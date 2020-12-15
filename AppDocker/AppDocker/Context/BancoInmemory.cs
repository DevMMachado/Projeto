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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuarios>()
                .HasKey(x => x.IdUser);

            modelBuilder.Entity<Phones>()
                .HasKey(x => x.IdPhone);

            modelBuilder.Entity<UsuarioPhone>()
                .HasKey(x => new { x.UsuariosId, x.PhonesId });
            modelBuilder.Entity<UsuarioPhone>()
                .HasOne(x => x.Usuario)
                .WithMany(m => m.Phone)
                .HasForeignKey(x => x.UsuariosId);
            modelBuilder.Entity<UsuarioPhone>()
                .HasOne(x => x.Phone)
                .WithMany(e => e.Usuario)
                .HasForeignKey(x => x.PhonesId);

            //var Phones = modelBuilder.Entity<Phones>().HasData(new Phones()
            //{
            //    IdPhone = Guid.NewGuid(),
            //    Number = "1111111111111",
            //    Ddd = "123",

            //});

            //modelBuilder.Entity<Usuarios>().HasData(new Usuarios()

            //{
            //    IdUser = Guid.NewGuid(),
            //    Name = "jose ",
            //    Email = "jose_m32@gmail.com",
            //    Password = "123",
                
                
            //});
             
            

         
            

    }

       
    }

    
}
