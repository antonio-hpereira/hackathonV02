using API_Loan_Simulator.Entities.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Loan_Simulator.Mapping
{

    public class ResultadoSimulacaoMap : IEntityTypeConfiguration<ResultadoSimulacaoViewModel>
    {
        public void Configure(EntityTypeBuilder<ResultadoSimulacaoViewModel> builder)
        {
            // Nome da tabela
            builder.ToTable("RESULTADO_SIMULACAO");           

            builder.Property(r => r.NU_NUMERO_PARCELAS)
                .HasColumnName("NU_NUMERO_PARCELAS")
                .IsRequired();

            builder.Property(r => r.VR_VALOR_AMORTIZADO)
                .HasColumnName("VR_VALOR_AMORTIZADO")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.VR_VALOR_JUROS)
                .HasColumnName("VR_VALOR_JUROS")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.VR_VALOR_PARCELAS)
                .HasColumnName("VR_VALOR_PARCELAS")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.VR_SALDO_DEVEDOR)
                .HasColumnName("VR_SALDO_DEVEDOR")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }

}
