
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.EventHub.IEventHub;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Core_Simulation.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace API_Loan_Simulator.EventHub
{
    public class SimulacaoEventProducer : ISimulatorEventProducer
    {

        private readonly string _connectionString;
        private readonly ISimulacaoEnvioRepository _envioRepository;
        public SimulacaoEventProducer(IConfiguration configuration, ISimulacaoEnvioRepository envioRepository)
        {
            _connectionString = configuration.GetConnectionString("ServiceBus");
            _envioRepository = envioRepository;
        }

        public async Task EnviarEventHubSimulacaoAsync(ResultadoFinalSimulacaoViewModel resultado)
        {
            if (await _envioRepository.JaFoiEnviadaAsync(resultado.CO_SIMULACAO_FINAL))
            {
                Console.WriteLine("Simulação já enviada anteriormente. Ignorando.");
                return;
            }

            var json = JsonSerializer.Serialize(resultado);
            var evento = new EventData(Encoding.UTF8.GetBytes(json));


            await using var producer = new EventHubProducerClient(_connectionString);
            using EventDataBatch batch = await producer.CreateBatchAsync();

            if (!batch.TryAdd(evento))
            {
                Console.WriteLine("Erro: evento excede o tamanho máximo do batch.");
                return;
            }

            await producer.SendAsync(batch);
            await _envioRepository.MarcarComoEnviadaAsync(resultado.CO_SIMULACAO_FINAL);

            Console.WriteLine("Simulação enviada com sucesso ao Event Hub.");
            
        }
    }



}
