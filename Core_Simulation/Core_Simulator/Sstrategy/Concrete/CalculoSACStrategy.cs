using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using API_Loan_Simulator.Entities.ViewModel;

namespace API_Loan_Simulator.Core_Simulator.Estrategy.Concrete
{
    public class CalculoSACStrategy : ICalculoParcelasStrategy
    {
        public Task<List<ResultadoSimulacaoViewModel>> Calcular(decimal valor, int meses, decimal taxaJurosMensal)
        {
            var resultado = new List<ResultadoSimulacaoViewModel>();
            decimal amortizacao = valor / meses;
            decimal saldoDevedor = valor;

            for (int i = 1; i <= meses; i++)
            {
                var juros = saldoDevedor * taxaJurosMensal;
                var parcela = amortizacao + juros;

                resultado.Add(new ResultadoSimulacaoViewModel
                {
                    NU_NUMERO_PARCELAS = i,
                    VR_VALOR_AMORTIZADO = Math.Round(amortizacao, 2),
                    VR_VALOR_JUROS = Math.Round(juros, 2),
                    VR_VALOR_PARCELAS = Math.Round(parcela, 2),
                    VR_SALDO_DEVEDOR = Math.Round(saldoDevedor - amortizacao, 2)
                });

                saldoDevedor -= amortizacao;
            }

            return Task.FromResult(resultado);
        }
    }


}
