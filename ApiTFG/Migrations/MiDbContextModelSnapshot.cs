﻿// <auto-generated />
using System;
using ApiTFG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiTFG.Migrations
{
    [DbContext(typeof(MiDbContext))]
    partial class MiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiTFG.Entidades.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Fin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("Ubicacion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Tipo")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Unidad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("ApiTFG.Entidades.InventarioChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("InventarioId")
                        .HasColumnType("int");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("InventarioChats");
                });

            modelBuilder.Entity("ApiTFG.Entidades.InventarioEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("InventarioId")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("InventarioEventos");
                });

            modelBuilder.Entity("ApiTFG.Entidades.TareaActualizacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cantidad")
                        .HasColumnType("float");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("TareaDetalleId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TareaDetalleId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("TareasActualizaciones");
                });

            modelBuilder.Entity("ApiTFG.Entidades.TareaDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cantidad")
                        .HasColumnType("float");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<bool>("Finalizada")
                        .HasColumnType("bit");

                    b.Property<string>("Unidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId")
                        .IsUnique();

                    b.ToTable("TareasDetalles");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Pregunta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Respuesta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Rol")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Evento", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Empresa", "Empresa")
                        .WithMany("Eventos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTFG.Entidades.Usuario", "Usuario")
                        .WithMany("Eventos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Inventario", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Empresa", "Empresa")
                        .WithMany("Inventarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("ApiTFG.Entidades.InventarioChat", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Inventario", "Inventario")
                        .WithMany("InventarioChats")
                        .HasForeignKey("InventarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTFG.Entidades.Usuario", "Usuario")
                        .WithMany("InventarioChats")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Inventario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiTFG.Entidades.InventarioEvento", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Inventario", "Inventario")
                        .WithMany("InventarioEventos")
                        .HasForeignKey("InventarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTFG.Entidades.Usuario", "Usuario")
                        .WithMany("InventarioEventos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Inventario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiTFG.Entidades.TareaActualizacion", b =>
                {
                    b.HasOne("ApiTFG.Entidades.TareaDetalle", "TareaDetalle")
                        .WithMany("Actualizaciones")
                        .HasForeignKey("TareaDetalleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTFG.Entidades.Usuario", "Usuario")
                        .WithMany("TareaActualizaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TareaDetalle");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiTFG.Entidades.TareaDetalle", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Evento", "Evento")
                        .WithOne("TareaDetalle")
                        .HasForeignKey("ApiTFG.Entidades.TareaDetalle", "EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Usuario", b =>
                {
                    b.HasOne("ApiTFG.Entidades.Empresa", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Empresa", b =>
                {
                    b.Navigation("Eventos");

                    b.Navigation("Inventarios");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Evento", b =>
                {
                    b.Navigation("TareaDetalle");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Inventario", b =>
                {
                    b.Navigation("InventarioChats");

                    b.Navigation("InventarioEventos");
                });

            modelBuilder.Entity("ApiTFG.Entidades.TareaDetalle", b =>
                {
                    b.Navigation("Actualizaciones");
                });

            modelBuilder.Entity("ApiTFG.Entidades.Usuario", b =>
                {
                    b.Navigation("Eventos");

                    b.Navigation("InventarioChats");

                    b.Navigation("InventarioEventos");

                    b.Navigation("TareaActualizaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
