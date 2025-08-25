using API_Loan_Simulator.Entities;

namespace API_Loan_Simulator.Repository.IRepository
{
    public interface IProdutoRepository
    {
        Task<Produto?> ObterProdutoParaSimulacaoAsync(decimal valorDesejado, int prazoMeses);
    }
}
