using Microsoft.EntityFrameworkCore;
using ApiTFG.Entidades;

namespace ApiTFG
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options)
            : base(options)
        {
        }

        // Agregar DbSets para cada entidad
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioEvento> InventarioEventos { get; set; }
        public DbSet<InventarioChat> InventarioChats { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<TareaDetalle> TareasDetalles { get; set; }
        public DbSet<TareaActualizacion> TareasActualizaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Empresa
            modelBuilder.Entity<Usuario>()
               .HasOne(u => u.Empresa)
               .WithMany(e => e.Usuarios)
               .HasForeignKey(u => u.EmpresaId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Empresa>()
                .HasIndex(p => p.Nombre)
                .IsUnique();
            #endregion
            
            #region Usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(p => p.Nombre)
                .IsUnique();
            #endregion

            #region Evento
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Eventos)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Evento>()
            .HasOne(e => e.TareaDetalle)
            .WithOne(td => td.Evento)
            .HasForeignKey<TareaDetalle>(td => td.EventoId)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TareaActualizacion>()
               .HasOne(ta => ta.TareaDetalle)
               .WithMany(td => td.Actualizaciones)
               .HasForeignKey(ta => ta.TareaDetalleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TareaActualizacion>()
               .HasOne(ta => ta.Usuario)
               .WithMany(u => u.TareaActualizaciones)
               .HasForeignKey(ta => ta.UsuarioId)
               .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Inventario
            modelBuilder.Entity<InventarioEvento>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.InventarioEventos)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<InventarioChat>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.InventarioChats)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Inventario>()
                .HasMany(i => i.InventarioEventos)
                .WithOne(i => i.Inventario)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Inventario>()
                .HasMany(i => i.InventarioChats)
                .WithOne(i => i.Inventario)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Empresa)
                .WithMany(e => e.Inventarios)
                .HasForeignKey(i => i.EmpresaId);
             #endregion
        }
    }
}
