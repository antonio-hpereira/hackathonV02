using API_Loan_Simulator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Loan_Simulator.Mapping
{   

    public class SimulacaoMap : IEntityTypeConfiguration<Simulacao>
    {
        public void Configure(EntityTypeBuilder<Simulacao> builder)
        {
            // Nome da tabela
            builder.ToTable("SIMULACAO");

            // Chave primária
            builder.HasKey(s => s.CO_SIMULACAO);

            // Propriedades
            builder.Property(s => s.CO_SIMULACAO)
                .HasColumnName("CO_SIMULACAO")
                .IsRequired();

            builder.Property(s => s.DT_DATA_SIMULACAO)
                .HasColumnName("DT_DATA_SIMULACAO")
                .IsRequired();

            builder.Property(s => s.VR_VALOR_ENTRADA)
                .HasColumnName("VR_VALOR_ENTRADA")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.NU_PARCELAS)
                .HasColumnName("NU_PARCELAS")
                .IsRequired();

            builder.Property(s => s.CO_PRODUTO)
               .HasColumnName("CO_PRODUTO")
               .IsRequired();

            builder.Property(s => s.NO_DESCRICAO_PRODUTO)
              .HasColumnName("NO_DESCRICAO_PRODUTO")
              .IsRequired();

            // Relacionamento: Simulacao -> Parcelas (1:N)
            builder.HasMany(s => s.Parcelas)
                .WithOne(p => p.Simulacao)
                .HasForeignKey(p => p.CO_SIMULACAO)
                .OnDelete(DeleteBehavior.Cascade); // ou Restrict, dependendo da lógica
        }
    }

}
