using API_Loan_Simulator.Entities.ViewModel;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace API_Loan_Simulator.Entities
{
    public class ListaSimulacoes
    {
        [JsonPropertyName("pagina")]
        public int NU_PAGINA { get; set; }

        [JsonPropertyName("qtRegistros")]
        public int QT_REGISTRO { get; set; }

        [JsonPropertyName("qtRegistrosPagina")]
        public int QT_REGISTRO_PAGINA { get; set; }

        [JsonPropertyName("registros")]
        public List<ListaSimulacaoViewModel> LT_REGISTRO { get; set; }
    }

}
