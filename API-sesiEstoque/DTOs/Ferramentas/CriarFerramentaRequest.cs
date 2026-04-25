using API_sesiEstoque.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace API_sesiEstoque.DTOs.Ferramentas
{
    public class CriarFerramentaRequest
    {
        [Required] 
        public string Nome { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public int QuantidadeTotal { get; set; }

        [Required]
        public int QuantidadeDisponivel { get; set; }

        [Required]
        public int? QuantiadeUso { get; set; }

        [Required]
        public int? QuantidadeManutencao { get; set; }

        
    }
}
