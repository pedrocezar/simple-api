using Simple.DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Simple.DDD.Infrastructure.Contexts
{
    public class ManutencaoContext : DbContext
    {
        public ManutencaoContext(DbContextOptions<ManutencaoContext> options) : base(options)
        { 
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Finalizacao> Finalizacoes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<OrdemServico> OrdensServicos { get; set; }
        public DbSet<Relacao> Relacoes { get; set; }
        public DbSet<Perfil> Perfis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasOne(s => s.Empresa).WithMany(s => s.Usuarios).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdemServico>().HasOne(s => s.Usuario).WithMany(s => s.OrdensServicos).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrdemServico>().HasOne(s => s.Finalizacao).WithMany(s => s.OrdensServicos).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relacao>().HasOne(s => s.OrdemServico).WithMany(s => s.Relacoes).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Relacao>().HasOne(s => s.Servico).WithMany(s => s.Relacoes).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
