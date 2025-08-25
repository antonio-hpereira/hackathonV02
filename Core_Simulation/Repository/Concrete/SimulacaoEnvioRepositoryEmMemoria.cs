using Core_Simulation.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Simulation.Repository.Concrete
{
    public class SimulacaoEnvioRepositoryEmMemoria : ISimulacaoEnvioRepository
    {
        private readonly HashSet<Guid> _idsEnviados = new();

        public Task<bool> JaFoiEnviadaAsync(Guid idSimulacao)
        {
            return Task.FromResult(_idsEnviados.Contains(idSimulacao));
        }

        public Task MarcarComoEnviadaAsync(Guid idSimulacao)
        {
            _idsEnviados.Add(idSimulacao);
            return Task.CompletedTask;
        }
    }

}
