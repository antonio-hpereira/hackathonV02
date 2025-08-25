using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Loan_Simulator.Entities.ViewModel
{
    public class ResultadoSimulacaoViewModel
    {
        [JsonPropertyName("parcela")]
        public int NU_NUMERO_PARCELAS { get; set; }

        [JsonPropertyName("valorAmortizado")]
        [JsonConverter(typeof(Decimal2CasasConverter))]
        public decimal VR_VALOR_AMORTIZADO { get; set; }
        [JsonPropertyName("valorJuros")]
        public decimal VR_VALOR_JUROS { get; set; }

        [JsonPropertyName("valorParcela")]
        public decimal VR_VALOR_PARCELAS { get; set; }

        [JsonPropertyName("saldoDevedor")]
        public decimal VR_SALDO_DEVEDOR { get; set; }


    }

}
public class Decimal2CasasConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetDecimal();

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        => writer.WriteNumberValue(Math.Round(value, 2));
}
