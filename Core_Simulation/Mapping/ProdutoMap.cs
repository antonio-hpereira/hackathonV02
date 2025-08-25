namespace API_Loan_Simulator.Mapping
{
    using API_Loan_Simulator.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Nome da tabela
            builder.ToTable("Produto");

            // Chave primária
            builder.HasKey(p => p.CO_PRODUTO);

            // Mapeamento das colunas
            builder.Property(p => p.CO_PRODUTO)
                .HasColumnName("CO_PRODUTO")
                .IsRequired();

            builder.Property(p => p.NO_PRODUTO)
                .HasColumnName("NO_PRODUTO")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.PC_TAXA_JUROS)
                .HasColumnName("PC_TAXA_JUROS")
                .HasColumnType("numeric(10,9)")
                .IsRequired();

            builder.Property(p => p.NU_MINIMO_MESES)
               .HasColumnName("NU_MINIMO_MESES")
               .IsRequired();

            builder.Property(p => p.NU_MAXIMO_MESES)
                .HasColumnName("NU_MAXIMO_MESES")
                .IsRequired(false); // Permite nulo
                

            builder.Property(p => p.VR_MINIMO)
                .HasColumnName("VR_MINIMO")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(p => p.VR_MAXIMO)
                .HasColumnName("VR_MAXIMO")
                .HasColumnType("numeric(18,2)")
                .IsRequired(false); // Permite nulo

           
        }

    }

}
