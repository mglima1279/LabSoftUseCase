using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProJ.Models;

public partial class DbClinicaContext : DbContext
{
    public DbClinicaContext()
    {
    }

    public DbClinicaContext(DbContextOptions<DbClinicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Consulta__06370DAD29CBE954");

            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.StatusConsulta)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Medico).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consulta_Medico");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consulta_Paciente");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Medico__06370DAD79C99400");

            entity.ToTable("Medico");

            entity.HasIndex(e => e.Crm, "UQ__Medico__C1FF83F72A62711D").IsUnique();

            entity.Property(e => e.Crm)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Especialidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Paciente__06370DAD5088229B");

            entity.ToTable("Paciente");

            entity.HasIndex(e => e.Cpf, "UQ__Paciente__C1FF930925B79C10").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
