using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Mapping;
using Microsoft.EntityFrameworkCore;

namespace API_Loan_Simulator.Context
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options)
        : base(options)
        {
        }
        public DbSet<Simulacao> Simulacoes { get; set; }
        public DbSet<Parcelas> Parcelas { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento 1:N entre Simulacao e Parcelas
            modelBuilder.Entity<Parcelas>()
                .HasOne(p => p.Simulacao)
                .WithMany(s => s.Parcelas)
                .HasForeignKey(p => p.CO_SIMULACAO)
                .OnDelete(DeleteBehavior.Cascade); // ou Restrict, se quiser evitar exclusão em cascata
                       
            modelBuilder.ApplyConfiguration(new ParcelasMap());
            modelBuilder.ApplyConfiguration(new SimulacaoMap()); 

            base.OnModelCreating(modelBuilder); // boa prática para manter herança funcional
        }

       
    }

}
