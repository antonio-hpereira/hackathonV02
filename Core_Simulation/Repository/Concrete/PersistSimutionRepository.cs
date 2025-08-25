using API_Loan_Simulator.Context;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.Repository.IRepository;

namespace API_Loan_Simulator.Repository.Concrete
{
    public class PersistSimutionRepository : IPersistSimutionRepository
    {
        private readonly MemoryContext _memoryContext;

        public PersistSimutionRepository(MemoryContext memoryContext)
        {
            _memoryContext = memoryContext;
        }

        public async Task PercistSimulation(ResultadoFinalSimulacaoViewModel resultado, DadosSimulacao dados)
        {
            var simulacao = new Simulacao();
            simulacao.CO_SIMULACAO = new Guid();
            simulacao.DT_DATA_SIMULACAO = DateTime.Now;
            simulacao.VR_VALOR_ENTRADA = dados.ValorDesejado;
            simulacao.NU_PARCELAS = dados.Prazo;
            simulacao.CO_PRODUTO = resultado.CO_PRODUTO;
            simulacao.NO_DESCRICAO_PRODUTO = resultado.NO_DESCRICAO_PRODUTO;
            simulacao.Parcelas = new List<Parcelas>();

            foreach (var item in resultado.SIMULACAO_SAC)
            {
                if (item.Parcelas != null)
                {
                    if (item.TipoParcela == "SAC")
                    {
                        foreach (var parcela in item.Parcelas)
                        {
                            var novaParcela = new Parcelas
                            {
                                NO_TIPO = item.TipoParcela,
                                NU_NUMERO_PARCELAS = parcela.NU_NUMERO_PARCELAS,
                                VR_VALOR_AMORTIZADO = parcela.VR_VALOR_AMORTIZADO,
                                VR_VALOR_JUROS = parcela.VR_VALOR_JUROS,
                                VR_VALOR_PARCELAS = parcela.VR_VALOR_PARCELAS,
                                VR_SALDO_DEVEDOR = parcela.VR_SALDO_DEVEDOR,

                            };
                            simulacao.Parcelas.Add(novaParcela);
                        }

                    }

                    if (item.TipoParcela == "PRICE")
                    {
                        foreach (var parcela in item.Parcelas)
                        {
                            var novaParcela = new Parcelas
                            {
                                NO_TIPO = item.TipoParcela,
                                NU_NUMERO_PARCELAS = parcela.NU_NUMERO_PARCELAS,
                                VR_VALOR_AMORTIZADO = parcela.VR_VALOR_AMORTIZADO,
                                VR_VALOR_JUROS = parcela.VR_VALOR_JUROS,
                                VR_VALOR_PARCELAS = parcela.VR_VALOR_PARCELAS,
                                VR_SALDO_DEVEDOR = parcela.VR_SALDO_DEVEDOR,

                            };
                            simulacao.Parcelas.Add(novaParcela);
                        }
                    }

                }
            }

            _memoryContext.Simulacoes.Add(simulacao);
            await _memoryContext.SaveChangesAsync();
        }
    }
}
