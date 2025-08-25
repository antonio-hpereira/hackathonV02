using API_Loan_Simulator.Context;
using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using API_Loan_Simulator.Core_Simulator.ISimulator;
using API_Loan_Simulator.Core_Simulator.Service;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.EventHub.IEventHub;
using API_Loan_Simulator.Repository.IRepository;

namespace API_Loan_Simulator.Core_Simulator
{
    public class SimuladorFinanciamento : ISimuladorFinanciamento
    {
        private const string connectionString = "Endpoint=sb://eventhack.servicebus.windows.net/;SharedAccessKeyName=hack;SharedAccessKey=HeHeVaVqyVkntO2FnjQcs2Ilh/4MUDo4y+AEhKp8z+g=;EntityPath=simulacoes";
                                                
        private readonly DBHack_Context _context;
        private readonly MemoryContext _memoryContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISimuladorParcelasRepository _simuladorParcelasRepository;
        private readonly ISimulatorEventProducer _simulatorEventProducer;
        private readonly IPersistSimulation _persistSimulation;
        private readonly ICalculoParcelasStrategy _calculoParcelasStrategy;
        private readonly SimuladorFinanciamentoService _simuladorFinanciamentoService;
        public SimuladorFinanciamento(DBHack_Context context,
                                               MemoryContext memoryContext,
                                               IProdutoRepository produtoRepository,
                                               ISimuladorParcelasRepository simuladorParcelasRepository,
                                               ISimulatorEventProducer simulatorEventProducer,
                                               IPersistSimulation persistSimulation,
                                               ICalculoParcelasStrategy calculoParcelasStrategy,
                                               SimuladorFinanciamentoService simuladorFinanciamentoService)
        {
            _context = context;
            _memoryContext = memoryContext;
            _produtoRepository = produtoRepository;
            _simuladorParcelasRepository = simuladorParcelasRepository;
            _simulatorEventProducer = simulatorEventProducer;
            _persistSimulation = persistSimulation;
            _calculoParcelasStrategy = calculoParcelasStrategy;
            _simuladorFinanciamentoService = simuladorFinanciamentoService;
        }




        public async Task<ResultadoFinalSimulacaoViewModel> SimularAsync(DadosSimulacao dados)
        {
            decimal valorDesejado = dados.ValorDesejado;
            int prazoMeses = dados.Prazo;

            if (valorDesejado <= 0)
                throw new ArgumentException("Valor desejado deve ser maior que zero.");
            if (prazoMeses <= 0)
                throw new ArgumentException("Prazo em meses deve ser maior que zero.");
            if (prazoMeses > 120)
                throw new ArgumentException("Prazo em meses não pode ser maior que 120 meses.");

            var produto = await _produtoRepository.ObterProdutoParaSimulacaoAsync(valorDesejado, prazoMeses);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado para os parâmetros informados.");


            var resultado = new ResultadoFinalSimulacaoViewModel
            {
                CO_SIMULACAO_FINAL = Guid.NewGuid(),
                CO_PRODUTO = produto.CO_PRODUTO,
                NO_DESCRICAO_PRODUTO = produto.NO_PRODUTO,
                VR_TAXA_JUROS = produto.PC_TAXA_JUROS,
                //SIMULACAO_SAC =_simuladorParcelasRepository.Calcular(valorDesejado, prazoMeses, produto.PC_TAXA_JUROS),
                SIMULACAO_SAC = await _simuladorFinanciamentoService.CalcularSimulacao(valorDesejado, prazoMeses, produto.PC_TAXA_JUROS)


            };

            _simulatorEventProducer.EnviarEventHubSimulacaoAsync(resultado).Wait(); 

            _persistSimulation.PercistSimulation(resultado, dados);

            return resultado;
        }

        
    }


}
