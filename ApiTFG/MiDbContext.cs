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
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioEvento> InventarioEventos { get; set; }
        public DbSet<InventarioChat> InventarioChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empresa)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.EmpresaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Horarios)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            #region Usuario
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Horarios)
                .WithMany(h => h.Usuarios)
                .UsingEntity(j => j.ToTable("UsuarioHorario"));
            modelBuilder.Entity<Empresa>()
                .HasIndex(p => p.Nombre)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(p => p.Nombre)
                .IsUnique();
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
