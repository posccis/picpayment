using Microsoft.EntityFrameworkCore;
using PicPayment.Domain.Domains;


namespace PicPayment.Persistence.Context
{
    public class PicPaymentContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public PicPaymentContext(DbContextOptions op) : base(op) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => new { u.Id, u.CPF, u.Email }).IsUnique();
            modelBuilder.Entity<Transferencia>().HasIndex(t => t.Id).IsUnique();
            modelBuilder.Entity<Transferencia>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.IdContaOrigem)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transferencia>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.IdContaDestino)
                .OnDelete(DeleteBehavior.Cascade);


            var usuarios = Seed.GetUsuarioSeed();
            var transferencias = Seed.GetTransferenciaSeed(usuarios);

            modelBuilder.Entity<Usuario>().HasData(usuarios);
            modelBuilder.Entity<Transferencia>().HasData(transferencias);

            base.OnModelCreating(modelBuilder);
            
        }


    }
}
