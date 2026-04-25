using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaUtilitario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Categoria { get; set; }

    public int Quantidade { get; set; }

    public bool IsAtivo { get; set; }

    public virtual ICollection<TabelaMovimentaco> TabelaMovimentacos { get; set; } = new List<TabelaMovimentaco>();

    public virtual ICollection<TabelaRetiradasUtilitario> TabelaRetiradasUtilitarios { get; set; } = new List<TabelaRetiradasUtilitario>();
}
