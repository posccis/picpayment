using Microsoft.EntityFrameworkCore;
using PicPayment.Domain.Domains;


namespace PicPayment.Persistence.Context
{
    public class PicPaymentContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ContaPF> ContasPF { get; set; }
        public DbSet<ContaPJ> ContasPJ { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public PicPaymentContext(DbContextOptions op) : base(op) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => new { u.Id, u.CPF, u.Email }).IsUnique();
            modelBuilder.Entity<Usuario>().HasMany(u => u.ContasPF).WithOne(c => c.Usuario).HasForeignKey(c => c.UsuarioId);
            modelBuilder.Entity<Usuario>().HasMany(u => u.ContasPJ).WithOne(c => c.Usuario).HasForeignKey(c => c.UsuarioId);
            modelBuilder.Entity<Usuario>().Property(u => u.CPF).HasMaxLength(11);
            var usuarios = Seed.GetUsuarioSeed();
            modelBuilder.Entity<Usuario>().HasData(usuarios);
            modelBuilder.Entity<ContaPF>().HasData(Seed.GetContaPFSeed(usuarios));
            modelBuilder.Entity<ContaPJ>().HasData(Seed.GetContaPJSeed(usuarios));

            base.OnModelCreating(modelBuilder);
            
        }


    }
}
