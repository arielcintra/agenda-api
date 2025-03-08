using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Domain.Entities
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        public string Telefone { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}