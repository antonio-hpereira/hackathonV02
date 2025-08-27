using API_Loan_Simulator.Context;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_Loan_Simulator.Repository.Concrete
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DBHack_Context _context;

        public ProdutoRepository(DBHack_Context context)
        {
            _context = context;
        }
                
        public async Task<Produto?> ObterProdutoParaSimulacaoAsync(decimal valorDesejado, int prazoMeses)
        {
            valorDesejado = Math.Round(valorDesejado, 2);           

            var resultado = await _context.Produtos
                .Where(p =>
                    p.VR_MINIMO <= valorDesejado &&
                    (p.VR_MAXIMO == null || p.VR_MAXIMO >= valorDesejado) &&
                    p.NU_MINIMO_MESES <= prazoMeses &&
                    (p.NU_MAXIMO_MESES == null || p.NU_MAXIMO_MESES >= prazoMeses))
                .OrderBy(p => p.PC_TAXA_JUROS) // opcional
                .FirstOrDefaultAsync();

            return resultado;
        }

       
    }
}
