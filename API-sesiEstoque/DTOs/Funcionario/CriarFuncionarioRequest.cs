using System.ComponentModel.DataAnnotations;

namespace API_sesiEstoque.DTOs.Funcionario
{
    public class CriarFuncionarioRequest
    {
        [Required, MinLength(3), MaxLength(150)]
        public string Nome {  get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        public string CPF { get; set; }


        [Required, MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        public string Nif { get; set; }

    }
}
