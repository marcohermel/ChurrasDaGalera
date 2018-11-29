using System.ComponentModel.DataAnnotations;

namespace WebApplicationChurras.Models
{
    public class Participante
    {
        [Key]
        public int ParticipanteID { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Valor contribuição")]
        public decimal? ValorContribuicao { get; set; }
        [Display(Name = "Pago?")]
        public bool Pago { get; set; }
        [Display(Name = "Com Bebida?")]
        public bool ComBebida { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public int ChurrascoID { get; set; }
        public Churrasco Churrasco { get; set; } 
    }
}
