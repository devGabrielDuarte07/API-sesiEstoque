using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaFerramenta
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public int QuantidadeTotal { get; set; }

    public int QuantidadeDisponivel { get; set; }

    public int? QuantidadeEmUso { get; set; }

    public int? QuantidadeManutencao { get; set; }

    public bool IsAtivo { get; set; }

    public virtual ICollection<TabelaEmprestimo> TabelaEmprestimos { get; set; } = new List<TabelaEmprestimo>();

    public virtual ICollection<TabelaMovimentaco> TabelaMovimentacos { get; set; } = new List<TabelaMovimentaco>();
}
