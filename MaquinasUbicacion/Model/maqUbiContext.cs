using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaquinasUbicacion.Model
{
    public class maqUbiContext : DbContext
    {
        public maqUbiContext(DbContextOptions<maqUbiContext> options) : base(options)
        {}

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Maquina> Maquina { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Cliente>().ToTable("Cliente");
            //modelBuilder.Entity<Maquina>().ToTable("Maquina");
            //modelBuilder.Entity<Ubicacion>().ToTable("Ubicacion");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id)
                    .UseSqlServerIdentityColumn()
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.nombre).IsRequired();
            });


            modelBuilder.Entity<Ubicacion>(entity => 
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id)
                    .UseSqlServerIdentityColumn()
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.ubicacion).IsRequired();
                entity.Property(e => e.idCliente).IsRequired();

                entity.HasOne(e => e.cliente)
                    .WithMany()
                    .HasForeignKey(e => e.idCliente)
                    .HasConstraintName("FK_Ubicacion_Cliente")
                    .IsRequired();

            });

            modelBuilder.Entity<Maquina>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id)
                    .UseSqlServerIdentityColumn()
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.modelo).IsRequired();
                entity.Property(e => e.tipo).IsRequired();

                entity.HasOne(e => e.cliente)
                    .WithMany()
                    .HasForeignKey(e => e.idCliente)
                    .HasConstraintName("FK_Maquina_Cliente")
                    .IsRequired();

                entity.HasOne(e => e.ubicacion)
                    .WithMany()
                    .HasForeignKey(e => e.idUbicacion)
                    .HasConstraintName("FK_Maquina_Ubicacion")
                    .IsRequired();

                //entity.HasIndex(e => e.idCliente).HasName("IX_FK_ClienteMaquina");
                //entity.HasIndex(e => e.idUbicacion).HasName("IX_FK_UbicacionMaquina");

            });
        }
    }

    
}
