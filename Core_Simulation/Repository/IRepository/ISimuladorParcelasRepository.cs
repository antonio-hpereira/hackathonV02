using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Repository.IRepository
{
    public interface ISimuladorParcelasRepository
    {
        List<TipoParcelasViewModel> Calcular(decimal valor, int meses, decimal taxaJurosMensal);
    }

}
