using API_Loan_Simulator.Core_Simulator.Estrategy.Concrete;
using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using Microsoft.Extensions.DependencyInjection;

namespace API_Loan_Simulator.Core_Simulator.Factory
{
    public class CalculoParcelasFactory : ICalculoParcelasFactory
    {
        private readonly IServiceProvider _provider;

        public CalculoParcelasFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ICalculoParcelasStrategy ObterStrategy(string tipo)
        {
            return tipo switch
            {
                "SAC" => _provider.GetRequiredService<CalculoSACStrategy>(),
                "PRICE" => _provider.GetRequiredService<CalculoPriceStrategy>(),
                _ => throw new ArgumentException("Tipo de cálculo inválido")
            };
        }
    }

}
