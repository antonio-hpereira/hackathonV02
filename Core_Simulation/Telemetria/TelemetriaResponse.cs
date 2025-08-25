namespace API_Loan_Simulator.Telemetria
{
    public class TelemetriaResponse
    {
        public DateTime DataReferencia { get; set; }
        public List<EndpointInfo> ListaEndpoints { get; set; } = new();
    }
}
