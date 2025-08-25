using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy
{
    public interface ICalculoParcelasStrategy
    {
        Task<List<ResultadoSimulacaoViewModel>> Calcular(decimal valor, int meses, decimal taxaJurosMensal);
    }
}
