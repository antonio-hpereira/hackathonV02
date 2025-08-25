using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Repository.IRepository
{
    public interface IPersistSimutionRepository
    {
        public Task PercistSimulation(ResultadoFinalSimulacaoViewModel resultado, DadosSimulacao dados);
    }
}
