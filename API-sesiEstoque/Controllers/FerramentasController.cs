using API_sesiEstoque.DTOs.Ferramentas;
using API_sesiEstoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_sesiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FerramentasController : ControllerBase
    {

        private readonly SesiEstoqueContext db;


        public FerramentasController(SesiEstoqueContext db)
        {
            this.db = db;
        }

        
        [HttpGet]
        public IActionResult GetFerramentas()
        {
            var ferramentas = db.TabelaFerramentas.Where(f => f.IsAtivo).Select(f => new DadosFerramentasResponse(f)).ToList();

            return Ok(ferramentas);
        }

        
        [HttpPost]
        public IActionResult CriarFerramenta(CriarFerramentaRequest dto)
        {
            if(db.TabelaFerramentas.Any(f => f.Codigo == dto.Codigo))
            {
                return BadRequest("Código já registrado");
            }

            var ferramenta = new TabelaFerramenta
            {
                Nome = dto.Nome,
                Codigo = dto.Codigo,
                QuantidadeTotal = dto.QuantidadeTotal,
                QuantidadeDisponivel = dto.QuantidadeDisponivel,
                QuantidadeEmUso = dto.QuantiadeUso,
                QuantidadeManutencao = dto.QuantidadeManutencao
            };

            db.TabelaFerramentas.Add(ferramenta);
            db.SaveChanges();

            return Ok("Ferramenta registrada com sucesso");
        }

    }
}
