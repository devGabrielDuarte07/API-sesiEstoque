using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaRetiradasUtilitario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int UtilitarioId { get; set; }

    public int QuantidadeRetirada { get; set; }

    public int? QuantidadeDevolvida { get; set; }

    public DateTime? DataRetirada { get; set; }

    public virtual TabelaUsuario Usuario { get; set; } = null!;

    public virtual TabelaUtilitario Utilitario { get; set; } = null!;
}
