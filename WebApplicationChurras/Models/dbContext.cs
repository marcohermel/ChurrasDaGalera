using Microsoft.EntityFrameworkCore;

namespace WebApplicationChurras.Models
{
    public class dbContext : DbContext
    {
        public DbSet<Churrasco> Churrascos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public dbContext() : base()
        { }
        public dbContext(DbContextOptions<dbContext> options): base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCore.Churras;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Churrasco>()
            .HasMany(c => c.Participantes)
            .WithOne(p => p.Churrasco);

            modelBuilder.Entity<Churrasco>()
                .Property(c => c.Descricao)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<Churrasco>()
              .Property(c => c.Observacao)
              .HasColumnType("varchar(4000)");

            modelBuilder.Entity<Participante>()
            .Property(p => p.Nome)
            .HasColumnType("varchar(200)");

            modelBuilder.Entity<Participante>()
             .Property(p => p.Observacao)
             .HasColumnType("varchar(4000)");

        }
    }
}
