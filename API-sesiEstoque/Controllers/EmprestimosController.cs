using API_sesiEstoque.DTOs.Emprestimos;
using API_sesiEstoque.Enums;
using API_sesiEstoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_sesiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly SesiEstoqueContext db;


        public EmprestimosController(SesiEstoqueContext db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public IActionResult RealizarEmprestimo(EmprestimoFerramentaRequest dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int id))
            {
                return null;
            }

            var ferramenta = db.TabelaFerramentas.FirstOrDefault(f => f.Id == dto.IdFerramenta);
            
            if(ferramenta == null)
            {
                return NotFound("Ferramenta não encontrada");
            }
            
            if (ferramenta.QuantidadeDisponivel < dto.Quantidade)
            {
                return NotFound("Quantidade indisponivel");
            }

            var emprestimoFerramenta = new TabelaEmprestimo
            {
                UsuarioId = id,
                FerramentaId = dto.IdFerramenta,
                Quantidade = dto.Quantidade,
                DataRetirada = DateTime.UtcNow
            };
            var emprestimoMovimentacao = new TabelaMovimentaco
            {
                Tipo = TipoMovimentacao.Emprestimo.ToString(),
                FerramentaId = dto.IdFerramenta,
                UsuarioId = id,
                Quantidade = dto.Quantidade,
                Data = DateTime.UtcNow
            };

            ferramenta.QuantidadeDisponivel -= dto.Quantidade;
            ferramenta.QuantidadeEmUso += dto.Quantidade;
            db.TabelaMovimentacoes.Add(emprestimoMovimentacao);
            db.TabelaEmprestimos.Add(emprestimoFerramenta);
            db.SaveChanges();
            return Ok("Emprestimo de ferramenta feita com sucesso");
        }
    }
}
