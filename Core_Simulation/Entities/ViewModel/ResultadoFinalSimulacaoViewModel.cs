using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Loan_Simulator.Entities.ViewModel
{
    public class ResultadoFinalSimulacaoViewModel
    {
        [JsonPropertyName("idsimulacao")]
        public Guid CO_SIMULACAO_FINAL { get; set; }       

        [JsonPropertyName("codigoProduto")]
        public int CO_PRODUTO { get; set; }

        [JsonPropertyName("descricaoProduto")]
        public string NO_DESCRICAO_PRODUTO { get; set; }

        [JsonPropertyName("taxaJuros")]
        public decimal VR_TAXA_JUROS { get; set; }

        [JsonPropertyName("simulacao_sac")]
        public List<TipoParcelasViewModel> SIMULACAO_SAC { get; set; }

       
    }

}
