using ReciboSueldo0506.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static ReciboSueldo0506.DAO.Mapping;

namespace ReciboSueldo0506.DAO
{
    public class ReciboSueldoContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<EstadoEmpleado> EstadoEmpelados { get; set; }

        public ReciboSueldoContext() : base("RECIBO")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmpleadoMapp());
            modelBuilder.Configurations.Add(new LoteMapp());
            modelBuilder.Configurations.Add(new ReciboMapp());
            modelBuilder.Configurations.Add(new EstadoEmpleadoMapp ());
            modelBuilder.Configurations.Add(new UsuariMapp());

            modelBuilder.Entity<Empleado>()
            .HasRequired<EstadoEmpleado>(s => s.Estado)
            .WithMany(g => g.Empleados)
            .HasForeignKey<int>(s => s.IdEstado);

            modelBuilder.Entity<Recibo>()
                .HasRequired<Empleado>(e => e.Empleado)
                .WithMany(r => r.Recibos)
                .HasForeignKey<int>(e => e.IdEmpleado);

            modelBuilder.Entity<Recibo>()
               .HasRequired<Lote>(e => e.Lote)
               .WithMany(r => r.Recibos)
               .HasForeignKey<int>(e => e.IdLote);

            modelBuilder.Entity<Usuario>().HasRequired(a => a.Empleado);
           




        }


    }
        
}