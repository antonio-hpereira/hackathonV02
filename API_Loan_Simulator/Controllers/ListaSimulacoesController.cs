using API_Loan_Simulator.Context;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Loan_Simulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaSimulacoesController : ControllerBase
    {
        MemoryContext _context;
        private readonly IListaSimulacaoRepository _listaSimulacaoRepositoryAsync;
        public ListaSimulacoesController(MemoryContext context, IListaSimulacaoRepository listaSimulacaoRepository)
        {
            _context = context;
            _listaSimulacaoRepositoryAsync = listaSimulacaoRepository;

        }


        [HttpGet("paginadas")]
        public async Task<IActionResult> ListarSimulacoesPaginadasAsync(
        [FromQuery] int pagina = 1,                
        [FromQuery] int qtdRegistros = 200,
        [FromQuery] int qtdregistrosPorPagina = 50
            
        )
        {
            if(pagina < 1 || qtdRegistros < 1 || qtdregistrosPorPagina < 1)            
                return BadRequest("Parâmetros inválidos. 'pagina', 'qtdRegistros' e 'qtdregistrosPorPagina' devem ser maiores que zero.");
            

            var resultado = await _listaSimulacaoRepositoryAsync
                            .ListarSimulacoesPaginadasAsync(pagina,
                            qtdRegistros, qtdregistrosPorPagina);

            if (resultado == null)            
                return NotFound("Nenhuma simulação encontrada.");           


                return Ok(resultado);
        }

        [HttpGet("volume_por_dia")]
        public async Task<IActionResult> GetSimulacoesPorProdutoEDiaAsync(
        [FromQuery] string data
       )
        {
            if (string.IsNullOrWhiteSpace(data))            
                return BadRequest("Parâmetros 'data' é obrigatório.");             

            var resultado = await _listaSimulacaoRepositoryAsync.ListarSimulacoesPorProdutoeDiaAsync(data);

            if (resultado == null || !resultado.simulacoes.Any())            
                return NotFound("Nenhuma simulação encontrada para os critérios informados.");
            

            return Ok(resultado);
        }



    }
}
