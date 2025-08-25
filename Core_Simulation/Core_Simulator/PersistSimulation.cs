using API_Loan_Simulator.Context;
using API_Loan_Simulator.Core_Simulator.ISimulator;
using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Entities.ViewModel;
using API_Loan_Simulator.Repository.IRepository;

namespace API_Loan_Simulator.Core_Simulator
{
    public class PersistSimulation : IPersistSimulation
    {
        private readonly MemoryContext _memoryContext;
        private readonly IPersistSimutionRepository _persistSimutionRepository;
        public PersistSimulation(MemoryContext memoryContext, IPersistSimutionRepository persistSimutionRepository)
        {
            _memoryContext = memoryContext;
            _persistSimutionRepository = persistSimutionRepository;
        }



        public void PercistSimulation(ResultadoFinalSimulacaoViewModel resultado, DadosSimulacao dados)
        {
            _persistSimutionRepository.PercistSimulation(resultado, dados);
        }
    }
}
