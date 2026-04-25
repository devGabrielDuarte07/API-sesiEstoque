using API_sesiEstoque.DTOs.Funcionario;
using API_sesiEstoque.Enums;
using API_sesiEstoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_sesiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private readonly SesiEstoqueContext db;

        public FuncionarioController(SesiEstoqueContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CriarFuncionario(CriarFuncionarioRequest dto)
        {
            if (db.TabelaUsuarios.Any(u => u.IsAtivo && u.Nif == dto.Nif)) 
            {
                return BadRequest("NIF já cadastrado");
            }

            if (db.TabelaUsuarios.Any(u => u.IsAtivo && u.Cpf == dto.CPF))
            {
                return BadRequest("CPF já cadastrado");
            }

            if (db.TabelaUsuarios.Any(u => u.IsAtivo && u.Email == dto.Email))
            {
                return BadRequest("Email já cadastrado");
            }

            var funcionario = new TabelaUsuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Cpf = dto.CPF,
                Tipo = TipoPerfil.F.ToString(),
                Senha = BCrypt.Net.BCrypt.HashPassword("Sesi2026"),
                Nif = dto.Nif
            };
            db.TabelaUsuarios.Add(funcionario);
            db.SaveChanges();
            return Ok("Funcionario criado com sucesso");
        }
    }
}
