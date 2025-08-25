using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Loan_Simulator.Entities
{
    public class Parcelas
    {
        [Key]
        public int CO_PARCELAS { get; set; }

        [JsonPropertyName("tipo")]
        public string NO_TIPO { get; set; }

        [JsonPropertyName("parcela")]
        public int NU_NUMERO_PARCELAS { get; set; }

        [JsonPropertyName("valorAmortizado")]
        public decimal VR_VALOR_AMORTIZADO { get; set; }

        [JsonPropertyName("valorJuros")]
        public decimal VR_VALOR_JUROS { get; set; }

        [JsonPropertyName("valorParcela")]
        public decimal VR_VALOR_PARCELAS { get; set; }

        [JsonPropertyName("saldoDevedor")]
        public decimal VR_SALDO_DEVEDOR { get; set; }

        // Chave estrangeira
        public Guid CO_SIMULACAO { get; set; }

        // Navegação: cada parcela pertence a uma simulação
        [JsonIgnore]
        public Simulacao Simulacao { get; set; }
    }
}
