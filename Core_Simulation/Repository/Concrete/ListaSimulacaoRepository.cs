using API_Loan_Simulator.Context;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_Loan_Simulator.Repository.Concrete
{
    public class ListaSimulacaoRepository: IListaSimulacaoRepository
    {
        private readonly MemoryContext _context;
       
        public ListaSimulacaoRepository(MemoryContext context)
        {
            _context = context;
            
        }

        public async Task<ListaSimulacoes> ListarSimulacoesPaginadasAsync(
        int pagina,
        int qtdRegistros,
        int qtdregistrosPorPagina
        )
        {            

            int skip = (pagina - 1) * qtdregistrosPorPagina;

            var simulacoes = await _context.Simulacoes
                .Include(s => s.Parcelas)
                .Skip(skip)
                .Take(qtdregistrosPorPagina)
                .ToListAsync();

            int totalPaginas = (int)Math.Ceiling((double)simulacoes.Count() / qtdregistrosPorPagina);

            if (pagina < 1 || pagina > totalPaginas)
                throw new ArgumentOutOfRangeException("Página inválida.");

            var totaisPorSimulacaoETipo = await _context.Parcelas
            .GroupBy(p => new { p.CO_SIMULACAO, p.NO_TIPO })
            .Select(g => new
            {
                SimulacaoId = g.Key.CO_SIMULACAO,
                Tipo = g.Key.NO_TIPO,
                ValorTotalParcelas = g.Sum(p => p.VR_VALOR_PARCELAS)
            })
            .ToListAsync();

            var registros = new List<ListaSimulacaoViewModel>();                                   

            ListaSimulacoes result = new  ListaSimulacoes();           

                    foreach (var simulacao in simulacoes)
                    {
                        var tipos = totaisPorSimulacaoETipo
                            .Where(t => t.SimulacaoId == simulacao.CO_SIMULACAO)
                            .ToList();

                        foreach (var tipo in tipos)
                        {
                            registros.Add(new ListaSimulacaoViewModel
                            {
                                idSimulacao = simulacao.CO_SIMULACAO,
                                tipo = tipo.Tipo,
                                valorDesejado = simulacao.VR_VALOR_ENTRADA,
                                prazo = simulacao.NU_PARCELAS,
                                valorTotalParcelas = tipo.ValorTotalParcelas
                            });
                        }
                    }

            return new ListaSimulacoes
                    {
                        NU_PAGINA = pagina,
                        QT_REGISTRO = qtdRegistros,
                        QT_REGISTRO_PAGINA = qtdregistrosPorPagina,
                        LT_REGISTRO = registros
                    };
                   

        }

        public async Task<ListaVolumeSimuladoViewModel> ListarSimulacoesPorProdutoeDiaAsync(string data)
        {

            DateTime dataFiltro = DateTime.Parse(data);
            var resultado = await _context.Simulacoes            
            .Include(s => s.Parcelas)
            .Where(sim => sim.DT_DATA_SIMULACAO.Date == dataFiltro)
            .Select(sim => new
            {
                sim.CO_SIMULACAO,
                sim.NO_DESCRICAO_PRODUTO,
                sim.CO_PRODUTO,
                taxaMediaJuro = sim.Parcelas
                    .Where(p => p.VR_SALDO_DEVEDOR + p.VR_VALOR_AMORTIZADO > 0)
                    .Average(p => p.VR_VALOR_JUROS / (p.VR_SALDO_DEVEDOR + p.VR_VALOR_AMORTIZADO)),

                valorMedioPrestacao = sim.Parcelas.Average(p => p.VR_VALOR_PARCELAS),
                valorTotalDesejado = sim.Parcelas.Sum(p => p.VR_VALOR_AMORTIZADO),
                valorTotalCredito = sim.Parcelas.Sum(p => p.VR_VALOR_PARCELAS)
            })
            .ToListAsync();

            ListaVolumeSimuladoViewModel lista = new ListaVolumeSimuladoViewModel();
            List<VolumeProduto> volumeProdutos = new List<VolumeProduto>();            

            lista.dataReferencia = dataFiltro.ToString("yyyy-MM-dd");            
            foreach (var sim in resultado)
            {
                if(sim != null)
                {
                    var volume = new VolumeProduto()
                    {
                        codigoProduto = sim.CO_PRODUTO,
                        descricaoProduto = sim.NO_DESCRICAO_PRODUTO,
                        taxaMediaJuros =Math.Round(sim.taxaMediaJuro,2),
                        valorMedioPrestacao = Math.Round(sim.valorMedioPrestacao,2),
                        valorTotalDesejado = Math.Round(sim.valorTotalDesejado,2),
                        valorTotalCredito = Math.Round(sim.valorTotalCredito, 2),
                    };
                    volumeProdutos.Add(volume);                    
                }
               
            }
            lista.simulacoes = volumeProdutos;

            return lista;
        }
    }
}
