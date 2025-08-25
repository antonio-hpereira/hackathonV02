namespace API_Loan_Simulator.Entities.ViewModel
{
    public class ListaSimulacaoViewModel
    {
        public Guid idSimulacao { get; set; }
        public string tipo { get; set; }
        public decimal valorDesejado { get; set; }
        public int prazo { get; set; }
        public decimal valorTotalParcelas { get; set; }


    }
}
