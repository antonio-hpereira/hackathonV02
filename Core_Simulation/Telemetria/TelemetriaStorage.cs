using System.Collections.Concurrent;

namespace API_Loan_Simulator.Telemetria
{
    public class TelemetriaStorage
    {
        private readonly ConcurrentDictionary<string, List<long>> _dados = new();

        public void Registrar(string endpoint, long tempoMs)
        {
            _dados.AddOrUpdate(endpoint,
                new List<long> { tempoMs },
                (_, lista) =>
                {
                    lista.Add(tempoMs);
                    return lista;
                });
        }

        public TelemetriaResponse ObterResumo()
        {
            var resposta = new TelemetriaResponse
            {
                DataReferencia = DateTime.UtcNow
            };

            foreach (var item in _dados)
            {
                var tempos = item.Value;
                resposta.ListaEndpoints.Add(new EndpointInfo
                {
                    NomeDaApi = item.Key,
                    QuantidadeRequisicoes = tempos.Count,
                    TempoMedio = tempos.Average(),
                    TempoMinimo = tempos.Min(),
                    TempoMaximo = tempos.Max()
                });
            }

            return resposta;
        }
    }

}
