using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Movie <-> Genre: many-to-many, explicit join table name
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.ToTable("MovieGenre"));

        // Movie <-> Person (actors): many-to-many, explicit join table name
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(p => p.ActedMovies)
            .UsingEntity(j => j.ToTable("MovieActor"));

        // Movie -> Person (director): one-to-many
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(p => p.DirectedMovies)
            .HasForeignKey(m => m.DirectorId)
            .OnDelete(DeleteBehavior.Restrict); // prevent deleting a person who directed movies
    }
}