namespace API_Loan_Simulator.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Produto
    {
        [Key]
        [Required]
        public int CO_PRODUTO { get; set; }

        [Required]
        [StringLength(200)]
        public string NO_PRODUTO { get; set; }

        [Required]
        [Column(TypeName = "numeric(10,9)")]
        public decimal PC_TAXA_JUROS { get; set; }

        [Required]
        public short NU_MINIMO_MESES { get; set; }
        
        public short? NU_MAXIMO_MESES { get; set; }
       

        [Required]
        [Column(TypeName = "numeric(10,2)")]
        public decimal VR_MINIMO { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal? VR_MAXIMO { get; set; }

      
    }


}
