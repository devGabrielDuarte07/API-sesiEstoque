using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaEmprestimo
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int FerramentaId { get; set; }

    public DateTime? DataRetirada { get; set; }

    public DateTime? DataDevolucao { get; set; }

    public virtual TabelaFerramenta Ferramenta { get; set; } = null!;

    public virtual TabelaUsuario Usuario { get; set; } = null!;
}
