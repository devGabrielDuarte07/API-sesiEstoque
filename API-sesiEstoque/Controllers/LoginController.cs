using API_sesiEstoque.DTOs.Auth;
using API_sesiEstoque.Enums;
using API_sesiEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_sesiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SesiEstoqueContext db;
        private readonly IConfiguration _config;

        public LoginController(SesiEstoqueContext db, IConfiguration config)
        {
            this.db = db;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = db.TabelaUsuarios.FirstOrDefault(u => u.Nif == dto.Nif);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
            {
                return Unauthorized("Nif ou senha inválidos");
            }

            string role = usuario.Tipo == TipoPerfil.A.ToString() ? "admin" : "funcionario";

            var claims = new List<Claim> {
                       new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });

        }

    }
}
