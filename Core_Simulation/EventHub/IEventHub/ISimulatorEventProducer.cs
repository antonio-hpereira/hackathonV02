using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.EventHub.IEventHub
{
    public interface ISimulatorEventProducer
    {
        Task EnviarEventHubSimulacaoAsync(ResultadoFinalSimulacaoViewModel resultado);
    }
}
