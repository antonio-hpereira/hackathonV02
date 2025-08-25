using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;

namespace API_Loan_Simulator.Core_Simulator.Factory
{
    public interface ICalculoParcelasFactory
    {
        ICalculoParcelasStrategy ObterStrategy(string tipo);
    }

}
