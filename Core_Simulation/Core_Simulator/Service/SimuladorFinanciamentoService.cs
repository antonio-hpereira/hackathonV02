using API_Loan_Simulator.Core_Simulator.Factory;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Core_Simulator.Service
{
    public class SimuladorFinanciamentoService
    {
        private readonly ICalculoParcelasFactory _factory;

        public SimuladorFinanciamentoService(ICalculoParcelasFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<TipoParcelasViewModel>> CalcularSimulacao(decimal valor, int meses, decimal taxa)
        {
            var lista = new List<TipoParcelasViewModel>();

            foreach (var tipo in new[] { "SAC", "PRICE" })
            {
                var strategy = _factory.ObterStrategy(tipo);
                var parcelas = await strategy.Calcular(valor, meses, taxa);

                lista.Add(new TipoParcelasViewModel
                {
                    TipoParcela = tipo,
                    Parcelas = parcelas
                });
            }

            return lista;
        }
    }

}
