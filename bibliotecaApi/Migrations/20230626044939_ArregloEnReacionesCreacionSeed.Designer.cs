﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bibliotecaApi.Models;

#nullable disable

namespace bibliotecaApi.Migrations
{
    [DbContext(typeof(BibliotecaDBContext))]
    [Migration("20230626044939_ArregloEnReacionesCreacionSeed")]
    partial class ArregloEnReacionesCreacionSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("bibliotecaApi.Models.Lector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Lector__3213E83F1DEED1A4");

                    b.ToTable("Lector", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9623908d-6eff-4f1e-9300-5a17b1369b1d"),
                            Nombre = "Ernesto"
                        },
                        new
                        {
                            Id = new Guid("753b8f78-0eeb-4b89-befe-de9d1aa97903"),
                            Nombre = "Carlos"
                        });
                });

            modelBuilder.Entity("bibliotecaApi.Models.Libro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Prestado")
                        .HasColumnType("bit")
                        .HasColumnName("prestado");

                    b.HasKey("Id")
                        .HasName("PK__Libro__3213E83F6980B911");

                    b.ToTable("Libro", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("afaa7d0c-62ac-4b94-b66a-87d71584a23f"),
                            ISBN = "1234567890",
                            Nombre = "Pinocho",
                            Prestado = false
                        });
                });

            modelBuilder.Entity("bibliotecaApi.Models.Prestamo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("FechaPrestamo")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IdLibro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LectorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Prestamo__3213E83F1980B911");

                    b.HasIndex("IdLibro");

                    b.HasIndex("LectorId");

                    b.ToTable("Prestamo", (string)null);
                });

            modelBuilder.Entity("bibliotecaApi.Models.Prestamo", b =>
                {
                    b.HasOne("bibliotecaApi.Models.Libro", "LibroNavigation")
                        .WithMany("Prestamos")
                        .HasForeignKey("IdLibro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Prest_Libro");

                    b.HasOne("bibliotecaApi.Models.Lector", "LectorNavigation")
                        .WithMany("Prestamos")
                        .HasForeignKey("LectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Prest_Lector");

                    b.Navigation("LectorNavigation");

                    b.Navigation("LibroNavigation");
                });

            modelBuilder.Entity("bibliotecaApi.Models.Lector", b =>
                {
                    b.Navigation("Prestamos");
                });

            modelBuilder.Entity("bibliotecaApi.Models.Libro", b =>
                {
                    b.Navigation("Prestamos");
                });
#pragma warning restore 612, 618
        }
    }
}