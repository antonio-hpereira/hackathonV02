using API_Loan_Simulator.Entities.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace API_Loan_Simulator.Entities
{
    public class Simulacao
    {
        [Key]
        public Guid CO_SIMULACAO { get; set; }
        public DateTime DT_DATA_SIMULACAO { get; set; }       
        public decimal VR_VALOR_ENTRADA { get; set; }
        public int NU_PARCELAS { get; set; }      
        public int CO_PRODUTO { get; set; }
        public string NO_DESCRICAO_PRODUTO { get; set; }

       
        // Navegação: uma simulação tem várias parcelas
        public List<Parcelas> Parcelas { get; set; }
    }




}

