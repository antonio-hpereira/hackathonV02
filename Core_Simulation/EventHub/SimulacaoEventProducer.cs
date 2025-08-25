
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.EventHub.IEventHub;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;
using System.Text.Json;

namespace API_Loan_Simulator.EventHub
{
    public class SimulacaoEventProducer : ISimulatorEventProducer
    {

        private const string connectionString = "Endpoint=sb://eventhack.servicebus.windows.net/;SharedAccessKeyName=hack;SharedAccessKey=HeHeVaVqyVkntO2FnjQcs2Ilh/4MUDo4y+AEhKp8z+g=;EntityPath=simulacoes";
        public async Task EnviarEventHubSimulacaoAsync(ResultadoFinalSimulacaoViewModel resultado)
        {

            var json = JsonSerializer.Serialize(resultado);
            var evento = new EventData(Encoding.UTF8.GetBytes(json));


            await using var producer = new EventHubProducerClient(connectionString);
            using EventDataBatch batch = await producer.CreateBatchAsync();
            batch.TryAdd(evento);

            await producer.SendAsync(batch);
            Console.WriteLine("Simulação enviada com sucesso ao Event Hub.");
        }
    }



}
