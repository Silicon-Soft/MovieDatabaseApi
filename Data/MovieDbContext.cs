using Microsoft.EntityFrameworkCore;
using MovieDatabaseApi.Models;

namespace MovieDatabaseApi.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; } = null;
    }
}
