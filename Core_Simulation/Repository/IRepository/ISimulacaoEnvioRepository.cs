using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Simulation.Repository.IRepository
{
    public interface ISimulacaoEnvioRepository
    {
        Task<bool> JaFoiEnviadaAsync(Guid idSimulacao);
        Task MarcarComoEnviadaAsync(Guid idSimulacao);
    }

}
