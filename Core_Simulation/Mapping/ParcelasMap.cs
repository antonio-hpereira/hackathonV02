using API_Loan_Simulator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Loan_Simulator.Mapping
{
  
    public class ParcelasMap : IEntityTypeConfiguration<Parcelas>
    {
        public void Configure(EntityTypeBuilder<Parcelas> builder)
        {
            // Nome da tabela
            builder.ToTable("PARCELAS");

            // Chave primária
            builder.HasKey(p => p.CO_PARCELAS);

            builder.Property(p => p.NO_TIPO)
               .HasColumnName("NO_TIPO")
               .IsRequired();

            // Propriedades
            builder.Property(p => p.CO_PARCELAS)
                .HasColumnName("CO_PARCELAS")
                .IsRequired();

            builder.Property(p => p.NU_NUMERO_PARCELAS)
                .HasColumnName("NU_NUMERO_PARCELAS")
                .IsRequired();

            builder.Property(p => p.VR_VALOR_AMORTIZADO)
                .HasColumnName("VR_VALOR_AMORTIZADO")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.VR_VALOR_JUROS)
                .HasColumnName("VR_VALOR_JUROS")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.VR_VALOR_PARCELAS)
                .HasColumnName("VR_VALOR_PARCELAS")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.VR_SALDO_DEVEDOR)
                .HasColumnName("VR_SALDO_DEVEDOR")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.CO_SIMULACAO)
                .HasColumnName("CO_SIMULACAO")
                .IsRequired();

            // Relacionamento: cada parcela pertence a uma simulação
            builder.HasOne(p => p.Simulacao)
                .WithMany(s => s.Parcelas)
                .HasForeignKey(p => p.CO_SIMULACAO)
                .OnDelete(DeleteBehavior.Cascade); // ou Restrict, conforme sua lógica
        }
    }

}
