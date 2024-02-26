﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseApi.Models;

namespace MovieDatabaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;
        public MovieController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Here we are getting the list of all the movies inside the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_dbContext == null)
                return NotFound();
            return await _dbContext.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (!_dbContext.Movies.Any())
                return NotFound();

            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return movie;

        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            if(_dbContext.Movies == null)
                return NotFound();

            var movie = await _dbContext.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();
            _dbContext.Entry(movie).State = EntityState.Modified;
            
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;

                }

            }

            return NoContent();
        }

        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
