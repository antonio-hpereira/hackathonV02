using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.Repository.IRepository;

namespace API_Loan_Simulator.Repository.Concrete
{
    public class SimuladorParcelasRepository : ISimuladorParcelasRepository
    {
        public List<TipoParcelasViewModel> Calcular(decimal valor, int meses, decimal taxaJurosMensal)
        {
            var lista = new List<TipoParcelasViewModel>();
            var ListParcela = new List<ResultadoSimulacaoViewModel>();
            decimal amortizacao = valor / meses;
            decimal saldoDevedor = valor;

            for (int i = 1; i <= meses; i++)
            {
                var juros = saldoDevedor * taxaJurosMensal;
                var parcela = amortizacao + juros;

                ListParcela.Add(new ResultadoSimulacaoViewModel
                {
                    NU_NUMERO_PARCELAS = i,
                    VR_VALOR_AMORTIZADO = Math.Round(amortizacao, 2),
                    VR_VALOR_JUROS = Math.Round(juros, 2),
                    VR_VALOR_PARCELAS = Math.Round(parcela, 2),
                    VR_SALDO_DEVEDOR = Math.Round(saldoDevedor - amortizacao, 2)
                });

                saldoDevedor -= amortizacao;
            }
            lista.Add(new TipoParcelasViewModel
            {
                TipoParcela = "SAC",
                Parcelas = ListParcela
            });


            var ListParcelaPrice = new List<ResultadoSimulacaoViewModel>();
            var fator = (decimal)Math.Pow(1 + (double)taxaJurosMensal, meses);
            var parcelaFixa = valor * taxaJurosMensal * fator / (fator - 1);

            for (int i = 1; i <= meses; i++)
            {
                var juros = saldoDevedor * taxaJurosMensal;
                var amortizacaoPrice = parcelaFixa - juros;

                ListParcelaPrice.Add(new ResultadoSimulacaoViewModel
                {
                    NU_NUMERO_PARCELAS = i,
                    VR_VALOR_AMORTIZADO = Math.Round(amortizacaoPrice, 2, MidpointRounding.AwayFromZero),
                    VR_VALOR_JUROS = Math.Round(juros, 2),
                    VR_VALOR_PARCELAS = Math.Round(parcelaFixa, 2),
                    VR_SALDO_DEVEDOR = Math.Round(saldoDevedor - amortizacaoPrice, 2, MidpointRounding.AwayFromZero)
                });

                saldoDevedor -= amortizacaoPrice;
            }

            lista.Add(new TipoParcelasViewModel
            {
                TipoParcela = "PRICE",
                Parcelas = ListParcela
            });


            return lista;
        }
    }
}
