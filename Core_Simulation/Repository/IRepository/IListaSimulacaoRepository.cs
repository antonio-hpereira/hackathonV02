using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Repository.IRepository
{
    public interface IListaSimulacaoRepository
    {
        public Task<ListaSimulacoes> ListarSimulacoesPaginadasAsync(
        int pagina,
        int qtdRegistros,
        int qtdregistrosPorPagina
        );

        public Task<ListaVolumeSimuladoViewModel> ListarSimulacoesPorProdutoeDiaAsync(
       string data            
       );


    }
}
