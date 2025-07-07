using BlogFecomercio.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogFecomercio.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Seguidor> Seguidores { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curtida>()
                .HasKey(c => new { c.UsuarioId, c.PostId }); 
            modelBuilder.Entity<Seguidor>()
                .HasOne(s => s.Usuario)
                .WithMany(u => u.Seguidores)
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seguidor>()
                .HasOne(s => s.SeguidorUsuario)
                .WithMany(u => u.Seguindo)
                .HasForeignKey(s => s.SeguidorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Curtida>()
               .HasOne(c => c.Post)
               .WithMany(p => p.Curtidas)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Curtida>()
               .HasOne(c => c.Usuario)
               .WithMany(u => u.Curtidas)
               .HasForeignKey(c => c.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
