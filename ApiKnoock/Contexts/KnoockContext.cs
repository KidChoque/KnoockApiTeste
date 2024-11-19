using System;
using System.Collections.Generic;
using ApiKnoock.Domains;
using Microsoft.EntityFrameworkCore;

namespace ApiKnoock.Contexts;

public partial class KnoockContext : DbContext
{
    public KnoockContext()
    {
    }

    public KnoockContext(DbContextOptions<KnoockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Afiliado> Afiliados { get; set; }

    public virtual DbSet<Condomino> Condominos { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Notificacao> Notificacaos { get; set; }

    public virtual DbSet<NotificacaoEntrega> NotificacaoEntregas { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //var connectionString = "Data Source=DUDU\\SQLEXPRESS; Initial Catalog=KNOOCK; Integrated Security=True; TrustServerCertificate=true";
            //optionsBuilder.UseSqlServer(connectionString);

            var connectionString = "Data Source=NOTE18-S21; Initial Catalog=KNOOCK; User Id=sa; Password=Senai@134; TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connectionString);

        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Afiliado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Afiliado__3214EC27B7F81958");

            entity.ToTable("Afiliado");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuario_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Afiliado__TipoUs__571DF1D5");
        });

        modelBuilder.Entity<Condomino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Condomin__3214EC27FF51F753");

            entity.ToTable("Condomino");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Apartamento).HasMaxLength(10);
            entity.Property(e => e.Bloco).HasMaxLength(5);
            entity.Property(e => e.DeliveryPin).HasMaxLength(10);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .HasColumnName("PIN");
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuario_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Condominos)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Condomino__TipoU__5BE2A6F2");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Entrega__3214EC27E95972C8");

            entity.ToTable("Entrega");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("datetime")
                .HasColumnName("Data_Notificacao");
            entity.Property(e => e.DataRegistro)
                .HasColumnType("datetime")
                .HasColumnName("Data_Registro");
            entity.Property(e => e.DataRetirada)
                .HasColumnType("datetime")
                .HasColumnName("Data_Retirada");
            entity.Property(e => e.FgEntrega).HasColumnName("FG_entrega");
            entity.Property(e => e.FotoProduto)
                .HasMaxLength(255)
                .HasColumnName("Foto_Produto");
            entity.Property(e => e.NotificacaoMorador)
                .HasDefaultValue(false)
                .HasColumnName("Notificacao_Morador");
            entity.Property(e => e.PinRetirada)
                .HasMaxLength(10)
                .HasColumnName("PIN_Retirada");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuario_ID");
            entity.Property(e => e.VeiculoId).HasColumnName("Veiculo_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__TipoUsu__6383C8BA");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.VeiculoId)
                .HasConstraintName("FK__Entrega__Veiculo__6477ECF3");
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC27A093E6F9");

            entity.ToTable("Notificacao");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DataNotificacao)
                .HasColumnType("datetime")
                .HasColumnName("Data_Notificacao");
            entity.Property(e => e.ImagemAviso)
                .HasColumnType("text")
                .HasColumnName("Imagem_Aviso");
            entity.Property(e => e.Mensagem).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuario_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Notificacaos)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__TipoU__693CA210");
        });

        modelBuilder.Entity<NotificacaoEntrega>(entity =>
        {
            entity.HasKey(e => e.NotificacaoId).HasName("PK__Notifica__73F653CA56C96FC4");

            entity.ToTable("Notificacao_Entrega");

            entity.Property(e => e.NotificacaoId)
                .ValueGeneratedNever()
                .HasColumnName("Notificacao_ID");
            entity.Property(e => e.EntregaId).HasColumnName("Entrega_ID");

            entity.HasOne(d => d.Entrega).WithMany(p => p.NotificacaoEntregas)
                .HasForeignKey(d => d.EntregaId)
                .HasConstraintName("FK__Notificac__Entre__6D0D32F4");

            entity.HasOne(d => d.Notificacao).WithOne(p => p.NotificacaoEntrega)
                .HasForeignKey<NotificacaoEntrega>(d => d.NotificacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__Notif__6C190EBB");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo__3214EC27B155A845");

            entity.ToTable("Tipo");

            entity.HasIndex(e => e.Tipo1, "UQ__Tipo__8E762CB4808ACCF1").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Tipo1)
                .HasMaxLength(20)
                .HasColumnName("Tipo");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo_Usu__3214EC276D386B62");

            entity.ToTable("Tipo_Usuario");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.TipoUsuarios)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tipo_Usua__Id_Ti__534D60F1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TipoUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tipo_Usua__Id_Us__52593CB8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27A5AE23CD");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534ACAF6778").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CodigoRecuperacao)
                .HasMaxLength(10)
                .HasColumnName("Codigo_Recuperacao");
            entity.Property(e => e.DataNascimento).HasColumnName("Data_Nascimento");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Endereco).HasMaxLength(255);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Senha).HasMaxLength(255);
            entity.Property(e => e.Telefone).HasMaxLength(15);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Veiculo__3214EC27AC2FBA79");

            entity.ToTable("Veiculo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Modelo).HasMaxLength(50);
            entity.Property(e => e.Placa).HasMaxLength(10);
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuario_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__TipoUsu__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
