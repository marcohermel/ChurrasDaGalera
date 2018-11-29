using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationChurras.Models
{
    public class Churrasco
    {
        [Key]
        public int ChurrascoID { get; set; }
        [Required(ErrorMessage ="Este campo é obrigatório")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Valor sugerido com bebida")]
        public decimal? ValorSugeridoComBebida { get; set; }
        [Display(Name = "Valor sugerido sem bebida")]
        public decimal? ValorSugeridoSemBebida { get; set; }
        public ICollection<Participante> Participantes { get; set; }
    }
}
