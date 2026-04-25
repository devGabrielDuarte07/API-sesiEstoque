using System.ComponentModel.DataAnnotations;

namespace API_sesiEstoque.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Nif {  get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
