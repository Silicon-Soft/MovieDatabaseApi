using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseApi.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; } = null;
    }
}
