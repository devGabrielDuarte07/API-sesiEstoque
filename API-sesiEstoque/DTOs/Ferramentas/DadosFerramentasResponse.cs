using API_sesiEstoque.Models;

namespace API_sesiEstoque.DTOs.Ferramentas
{
    public class DadosFerramentasResponse
    {
        public string Nome {  get; set; }
        public string Codigo { get; set; } 
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int? QuantiadeUso { get; set; }
        public int? QuantidadeManutencao { get; set; }

        public DadosFerramentasResponse(TabelaFerramenta ferramenta)
        {
            Nome = ferramenta.Nome;
            Codigo = ferramenta.Codigo;
            QuantidadeTotal = ferramenta.QuantidadeTotal;
            QuantidadeDisponivel = ferramenta.QuantidadeDisponivel;
            QuantiadeUso = ferramenta.QuantidadeEmUso;
            QuantidadeManutencao = ferramenta.QuantidadeManutencao;
        }
    }
}
