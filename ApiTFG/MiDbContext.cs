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
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
        }
    }
}
