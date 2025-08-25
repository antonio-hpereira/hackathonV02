using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Core_Simulator.Estrategy.Concrete
{
    public class CalculoPriceStrategy : ICalculoParcelasStrategy
    {
        public Task<List<ResultadoSimulacaoViewModel>> Calcular(decimal valor, int meses, decimal taxaJurosMensal)
        {
            var resultado = new List<ResultadoSimulacaoViewModel>();
            var fator = (decimal)Math.Pow(1 + (double)taxaJurosMensal, meses);
            var parcelaFixa = valor * taxaJurosMensal * fator / (fator - 1);
            var saldoDevedor = valor;

            for (int i = 1; i <= meses; i++)
            {
                var juros = saldoDevedor * taxaJurosMensal;
                var amortizacao = parcelaFixa - juros;

                resultado.Add(new ResultadoSimulacaoViewModel
                {
                    NU_NUMERO_PARCELAS = i,
                    VR_VALOR_AMORTIZADO = Math.Round(amortizacao, 2),
                    VR_VALOR_JUROS = Math.Round(juros, 2),
                    VR_VALOR_PARCELAS = Math.Round(parcelaFixa, 2),
                    VR_SALDO_DEVEDOR = Math.Round(saldoDevedor - amortizacao, 2)
                });

                saldoDevedor -= amortizacao;
            }

            return Task.FromResult(resultado);
        }
    }

}
