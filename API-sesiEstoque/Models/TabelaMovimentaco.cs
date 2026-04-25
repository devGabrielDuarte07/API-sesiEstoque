using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaMovimentaco
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public int? FerramentaId { get; set; }

    public int? UtilitarioId { get; set; }

    public int? UsuarioId { get; set; }

    public int? Quantidade { get; set; }

    public string? Observacao { get; set; }

    public DateTime? Data { get; set; }

    public virtual TabelaFerramenta? Ferramenta { get; set; }

    public virtual TabelaUsuario? Usuario { get; set; }

    public virtual TabelaUtilitario? Utilitario { get; set; }
}
