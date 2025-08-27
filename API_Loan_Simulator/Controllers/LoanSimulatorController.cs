using API_Loan_Simulator.Context;
using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using API_Loan_Simulator.Core_Simulator.ISimulator;
using API_Loan_Simulator.Core_Simulator.Service;
using API_Loan_Simulator.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Loan_Simulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanSimulatorController : ControllerBase
    {
        DBHack_Context _context;
        private readonly ISimuladorFinanciamento _simulador;
        private readonly ICalculoParcelasStrategy _calculoParcelasStrategy;
       
        public LoanSimulatorController(DBHack_Context context,
                                        ISimuladorFinanciamento simulador,
                                        ICalculoParcelasStrategy calculoParcelasStrategy
                                       )
        {
            _context = context;
            _simulador = simulador;
            _calculoParcelasStrategy = calculoParcelasStrategy;
            
        }



        // POST api/<LoanSimulatorController>
        [HttpPost("simulacoes")]
        /// <summary>
        /// Realiza a simulação de financiamento com base nos dados fornecidos.
        /// </summary>
        /// <param name="dados">Dados da simulação</param>
        /// <returns>Lista de tipos de parcelas simuladas</returns>
        public async Task<IActionResult> CalcularSimulacao([FromBody] DadosSimulacao dados)
        {
            if (dados == null)
                return BadRequest("Dados da simulação não podem ser nulos.");

            if (dados.ValorDesejado <= 0 || dados.Prazo <= 0 )
                return BadRequest("Parâmetros inválidos para simulação.");

            var resultados = await _simulador.SimularAsync(dados);

            return Ok(resultados);


        }

        
    }
}
