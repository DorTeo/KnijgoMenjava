using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Models;


namespace KnjigoMenjava.Data;

public class AppDbContext : IdentityDbContext<Uporabnik>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Knjiga> Knjige => Set<Knjiga>();
    public DbSet<Kategorija> Kategorije => Set<Kategorija>();
    public DbSet<Rezervacija> Rezervacije => Set<Rezervacija>();
    public DbSet<Ocena> Ocene => Set<Ocena>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        

        modelBuilder.Entity<Knjiga>().ToTable("Knjiga");
        modelBuilder.Entity<Kategorija>().ToTable("Kategorija");
        modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");
        modelBuilder.Entity<Ocena>().ToTable("Ocena");
        modelBuilder.Entity<Uporabnik>().ToTable("Uporabnik");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<Ocena>()
            .HasOne(o => o.Knjiga)
            .WithMany()
            .HasForeignKey(o => o.KnjigaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Rezervacija>()
            .HasOne(r => r.Knjiga)
            .WithMany()
            .HasForeignKey(r => r.KnjigaId)
            .OnDelete(DeleteBehavior.Cascade);

    }


}
