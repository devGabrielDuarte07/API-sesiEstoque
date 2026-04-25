using System;
using System.Collections.Generic;

namespace API_sesiEstoque.Models;

public partial class TabelaUsuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Nif { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public bool IsAtivo { get; set; }

    public DateTime? CriadoEm { get; set; }

    public virtual ICollection<TabelaEmprestimo> TabelaEmprestimos { get; set; } = new List<TabelaEmprestimo>();

    public virtual ICollection<TabelaMovimentaco> TabelaMovimentacos { get; set; } = new List<TabelaMovimentaco>();

    public virtual ICollection<TabelaRetiradasUtilitario> TabelaRetiradasUtilitarios { get; set; } = new List<TabelaRetiradasUtilitario>();
}
