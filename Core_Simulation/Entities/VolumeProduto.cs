namespace API_Loan_Simulator.Entities
{
    public class VolumeProduto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public decimal taxaMediaJuros { get; set; }
        public decimal valorMedioPrestacao { get; set; }
        public decimal valorTotalDesejado { get; set; }
        public decimal valorTotalCredito { get; set; }
    }
}
