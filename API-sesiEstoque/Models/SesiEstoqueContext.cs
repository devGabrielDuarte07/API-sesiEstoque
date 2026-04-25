using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace API_sesiEstoque.Models;

public partial class SesiEstoqueContext : DbContext
{
    public SesiEstoqueContext()
    {
    }

    public SesiEstoqueContext(DbContextOptions<SesiEstoqueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TabelaEmprestimo> TabelaEmprestimos { get; set; }

    public virtual DbSet<TabelaFerramenta> TabelaFerramentas { get; set; }

    public virtual DbSet<TabelaMovimentaco> TabelaMovimentacoes { get; set; }

    public virtual DbSet<TabelaRetiradasUtilitario> TabelaRetiradasUtilitarios { get; set; }

    public virtual DbSet<TabelaUsuario> TabelaUsuarios { get; set; }

    public virtual DbSet<TabelaUtilitario> TabelaUtilitarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=sesi_estoque;user=root;password=senai", Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.0.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TabelaEmprestimo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_emprestimos");

            entity.HasIndex(e => e.FerramentaId, "fk_emprestimo_ferramenta");

            entity.HasIndex(e => e.UsuarioId, "fk_emprestimo_usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DataDevolucao)
                .HasColumnType("datetime")
                .HasColumnName("data_devolucao");
            entity.Property(e => e.DataRetirada)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("data_retirada");
            entity.Property(e => e.FerramentaId)
                .HasColumnType("int(11)")
                .HasColumnName("ferramenta_id");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.Ferramenta).WithMany(p => p.TabelaEmprestimos)
                .HasForeignKey(d => d.FerramentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_emprestimo_ferramenta");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TabelaEmprestimos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_emprestimo_usuario");
        });

        modelBuilder.Entity<TabelaFerramenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_ferramentas");

            entity.HasIndex(e => e.Codigo, "codigo").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.IsAtivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_ativo");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.QuantidadeDisponivel)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_disponivel");
            entity.Property(e => e.QuantidadeEmUso)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_em_uso");
            entity.Property(e => e.QuantidadeManutencao)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_manutencao");
            entity.Property(e => e.QuantidadeTotal)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_total");
        });

        modelBuilder.Entity<TabelaMovimentaco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_movimentacoes");

            entity.HasIndex(e => e.FerramentaId, "fk_movimentacao_ferramenta");

            entity.HasIndex(e => e.UsuarioId, "fk_movimentacao_usuario");

            entity.HasIndex(e => e.UtilitarioId, "fk_movimentacao_utilitario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Data)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("data");
            entity.Property(e => e.FerramentaId)
                .HasColumnType("int(11)")
                .HasColumnName("ferramenta_id");
            entity.Property(e => e.Observacao)
                .HasMaxLength(255)
                .HasColumnName("observacao");
            entity.Property(e => e.Quantidade)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.UtilitarioId)
                .HasColumnType("int(11)")
                .HasColumnName("utilitario_id");

            entity.HasOne(d => d.Ferramenta).WithMany(p => p.TabelaMovimentacos)
                .HasForeignKey(d => d.FerramentaId)
                .HasConstraintName("fk_movimentacao_ferramenta");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TabelaMovimentacos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("fk_movimentacao_usuario");

            entity.HasOne(d => d.Utilitario).WithMany(p => p.TabelaMovimentacos)
                .HasForeignKey(d => d.UtilitarioId)
                .HasConstraintName("fk_movimentacao_utilitario");
        });

        modelBuilder.Entity<TabelaRetiradasUtilitario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_retiradas_utilitarios");

            entity.HasIndex(e => e.UsuarioId, "fk_retirada_usuario");

            entity.HasIndex(e => e.UtilitarioId, "fk_retirada_utilitario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DataRetirada)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("data_retirada");
            entity.Property(e => e.QuantidadeDevolvida)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_devolvida");
            entity.Property(e => e.QuantidadeRetirada)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade_retirada");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.UtilitarioId)
                .HasColumnType("int(11)")
                .HasColumnName("utilitario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TabelaRetiradasUtilitarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_retirada_usuario");

            entity.HasOne(d => d.Utilitario).WithMany(p => p.TabelaRetiradasUtilitarios)
                .HasForeignKey(d => d.UtilitarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_retirada_utilitario");
        });

        modelBuilder.Entity<TabelaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_usuario");

            entity.HasIndex(e => e.Cpf, "cpf").IsUnique();

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Nif, "nif").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");
            entity.Property(e => e.CriadoEm)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("criado_em");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.IsAtivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_ativo");
            entity.Property(e => e.Nif)
                .HasMaxLength(20)
                .HasColumnName("nif");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TabelaUtilitario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tabela_utilitarios");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.IsAtivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_ativo");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Quantidade)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
