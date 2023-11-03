using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class FlunaGn3Context : DbContext
{
    public FlunaGn3Context()
    {
    }

    public FlunaGn3Context(DbContextOptions<FlunaGn3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CatalogoDepartamento> CatalogoDepartamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Sueldo> Sueldos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VJ5G6F0\\FLUNA; Database= FLunaGN3; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=Password1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogoDepartamento>(entity =>
        {
            entity.HasKey(e => e.ClaveDepartamento).HasName("PK__Catalogo__2A9108F8A50A74E8");

            entity.Property(e => e.ClaveDepartamento).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.ClaveEmpleado).HasName("PK__Empleado__127A686250E474A6");

            entity.Property(e => e.Departamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sueldo>(entity =>
        {
            entity.HasKey(e => e.Idsueldo).HasName("PK__Sueldos__6B0978F315C20508");

            entity.Property(e => e.Idsueldo).HasColumnName("IDSueldo");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SueldoMensual).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ClaveEmpleadoNavigation).WithMany(p => p.Sueldos)
                .HasForeignKey(d => d.ClaveEmpleado)
                .HasConstraintName("FK__Sueldos__ClaveEm__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
