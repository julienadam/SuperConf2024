using Microsoft.EntityFrameworkCore;

namespace SuperConf2024.Entities;

public partial class SuperconfdbContext : DbContext
{
    public SuperconfdbContext()
    {
    }

    public SuperconfdbContext(DbContextOptions<SuperconfdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=AZURE_SQL_CONNECTIONSTRING", o => o.UseAzureSqlDefaults());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inscription>(entity =>
        {
            entity.HasKey(e => e.InscriptionId).HasName("PK_Inscription");

            entity.ToTable("inscription");

            entity.Property(e => e.ChoixRepas)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DateNaissance).HasColumnType("datetime");
            entity.Property(e => e.DemandeParticuliere).HasMaxLength(180);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
