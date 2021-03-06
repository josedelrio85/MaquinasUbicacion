﻿// <auto-generated />
using MaquinasUbicacion.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MaquinasUbicacion.Migrations
{
    [DbContext(typeof(maqUbiContext))]
    partial class maqUbiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MaquinasUbicacion.Model.Cliente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("MaquinasUbicacion.Model.Maquina", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idUbicacion");

                    b.Property<string>("modelo")
                        .IsRequired();

                    b.Property<string>("nSerie");

                    b.Property<string>("tipo")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("idUbicacion");

                    b.ToTable("Maquina");
                });

            modelBuilder.Entity("MaquinasUbicacion.Model.Ubicacion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idCliente");

                    b.Property<string>("ubicacion")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("idCliente");

                    b.ToTable("Ubicacion");
                });

            modelBuilder.Entity("MaquinasUbicacion.Model.Maquina", b =>
                {
                    b.HasOne("MaquinasUbicacion.Model.Ubicacion", "ubicacion")
                        .WithMany()
                        .HasForeignKey("idUbicacion")
                        .HasConstraintName("FK_Maquina_Ubicacion")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaquinasUbicacion.Model.Ubicacion", b =>
                {
                    b.HasOne("MaquinasUbicacion.Model.Cliente", "cliente")
                        .WithMany()
                        .HasForeignKey("idCliente")
                        .HasConstraintName("FK_Ubicacion_Cliente")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
