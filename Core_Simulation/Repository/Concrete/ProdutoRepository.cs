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
            return await _context.Produtos
                .FirstOrDefaultAsync(p =>
                    p.VR_MINIMO <= valorDesejado &&
                    (p.VR_MAXIMO == null || p.VR_MAXIMO >= valorDesejado) &&
                    p.NU_MAXIMO_MESES >= prazoMeses);
        }

       
    }
}
