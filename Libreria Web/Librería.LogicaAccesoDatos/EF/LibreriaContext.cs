using Libreria.LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librería.LogicaAccesoDatos.EF
{
    public class LibreriaContext: DbContext
    {
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<TipoCabania> TiposCabanias { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Cabania> Cabanias { get; set; }
        public DbSet<Parametro> Parametros { get; set; }

        public LibreriaContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoCabania>()
                .OwnsOne(tp => tp.Nombre, nomTc =>
                {
                    nomTc.Property(d => d.ValorNombre).HasColumnName("NombreDelTipo");
                    nomTc.HasIndex(d => new { d.ValorNombre }).IsUnique() ;

                });

            modelBuilder.Entity<Cabania>()
                .OwnsOne(cab => cab.Nombre, nomCab => {

                    nomCab.Property(c => c.Valor).HasColumnName("ValorNombre");
                    nomCab.HasIndex(c => new {c.Valor }).IsUnique() ;
                
                });
        }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"SERVER=(localdb)\MSSQLLocaldb;  DATABASE = Obligatorio1P3; INTEGRATED SECURITY=TRUE; ENCRYPT= False;")
        //        .EnableDetailedErrors() ;

        //}
    }
}
