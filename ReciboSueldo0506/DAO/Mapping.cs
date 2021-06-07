using ReciboSueldo0506.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ReciboSueldo0506.DAO
{
    public class Mapping
    {
        public class EmpleadoMapp : EntityTypeConfiguration<Empleado>
        {
            public EmpleadoMapp()
            {
                this.ToTable("EMPLEADO");

                this.HasKey(m => m.IdEmpleado);
                this.Property(m => m.IdEmpleado)
                .HasColumnName("IDEMPLEADO")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



                this.Property(m => m.Nombre)
                .HasColumnName("NOMBRE")
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Apellido)
                .HasColumnName("APELLIDO")
                .HasColumnOrder(3)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Cuil)
                .HasColumnName("CUIL")
                .HasColumnOrder(4)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Mail)
                .HasColumnName("MAIL")
                .HasColumnOrder(5)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Celular)
                .HasColumnName("CELULAR")
                .HasColumnOrder(6)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");
            }

        }

        public class LoteMapp : EntityTypeConfiguration<Lote>
        {
            public LoteMapp()
            {
                this.ToTable("LOTE");

                this.HasKey(m => m.IdLote);
                this.Property(m => m.IdLote)
                .HasColumnName("IDLOTE")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                this.Property(m => m.Mes)
                .HasColumnName("MES")
                .HasColumnOrder(2)
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Anio)
                .HasColumnName("ANIO")
                .HasColumnOrder(3)
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.Concepto)
                .HasColumnName("CONCEPTO")
                .HasColumnOrder(4)
                .HasColumnType("NVARCHAR(MAX)");

                this.Property(m => m.FecAlta)
               .HasColumnName("FECALTA")
               .IsRequired()
               .HasColumnOrder(5)
               .HasColumnType("DATE");

            }

        }

        public class ReciboMapp : EntityTypeConfiguration<Recibo>
        {
            public ReciboMapp()
            {
                this.ToTable("RECIBO");

                this.HasKey(m => m.IdRecibo);
                this.Property(m => m.IdRecibo)
                .HasColumnName("IDRECIBO")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                this.Property(m => m.Archivo)
                .HasColumnName("ARCHIVO")
                .HasColumnOrder(2)
                .IsRequired();




            }

        }

        public class EstadoEmpleadoMapp : EntityTypeConfiguration<EstadoEmpleado>
        {
            public EstadoEmpleadoMapp()
            {
                this.ToTable("ESTADOEMPELADO");

                this.HasKey(m => m.IdEstadoEmpleado);
                this.Property(m => m.IdEstadoEmpleado)
                .HasColumnName("IDESTADOEMPELADO")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                this.Property(m => m.DescEstadoEmpleado)
                .HasColumnName("DESCESTADOEMPELADO")
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");
            }

        }

        public class UsuariMapp : EntityTypeConfiguration<Usuario>
        {
            public UsuariMapp()
            {
                this.ToTable("USUARIO");

                this.HasKey(m => m.IdUsuario);
                this.Property(m => m.IdUsuario)
                .HasColumnName("IDUSUARIO")
                .HasColumnOrder(1)
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                this.Property(m => m.IdEmpleado)
                .HasColumnName("IDEMPLEADO")
                .HasColumnOrder(2)
                .HasColumnType("INT");

                this.Property(m => m.UserName)
                .HasColumnName("USERNAME")
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");


            }
        }
    }
}