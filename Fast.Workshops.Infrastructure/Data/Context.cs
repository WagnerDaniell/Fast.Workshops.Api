using Fast.Workshops.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<WorkshopColaborador> WorkshopColaboradores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(u => u.Password)
                .IsRequired();

            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<WorkshopColaborador>(entity =>
        {
            entity.ToTable("workshop_colaboradores");

            entity.HasKey(wc => new { wc.WorkshopId, wc.ColaboradorId });

            entity.HasOne(wc => wc.Workshop)
                .WithMany(w => w.WorkshopColaboradores)
                .HasForeignKey(wc => wc.WorkshopId);

            entity.HasOne(wc => wc.Colaborador)
                .WithMany(c => c.WorkshopColaboradores)
                .HasForeignKey(wc => wc.ColaboradorId);
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.ToTable("workshops");
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
            entity.Property(w => w.Description).HasMaxLength(500);
            entity.Property(w => w.Date).IsRequired();
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.ToTable("colaboradores");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });
    }
}