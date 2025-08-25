using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Core_Simulator.ISimulator
{
    public interface ISimuladorFinanciamento
    {
       Task<ResultadoFinalSimulacaoViewModel> SimularAsync(DadosSimulacao dados);
    }

}
