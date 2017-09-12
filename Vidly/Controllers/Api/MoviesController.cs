using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.DTOs;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET api/<controller>
        //query parameter in case this action is called from rental
        //this returns a DTO with movies, the result depends wether we have a query or not
        public IHttpActionResult GetMovies(string query = null)
        {
            //we have a queryable variable moviesquery (results from dbcontext)
            //let's read the movies without any query filter
            //the only filter is to get only available movies
            var moviesquery = _context.Movies
                .Include(c => c.Genre)
                .Where(c=>c.NumberAvailable>0);

            //now let's assume we have a query
            if (!String.IsNullOrWhiteSpace(query))
                moviesquery = _context.Movies.Where(c => c.Name.Contains(query));

            //wether we had a query or not, the results (moviesquery) have to be mapped into the dto
            //and returned to the view
            var movieDto = moviesquery.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDto);
        }

        // GET api/<controller>/5
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            var movieDto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = RoleNames.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.NumberAvailable = movie.NumberInStock;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id),movieDto);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Authorize(Roles = RoleNames.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c=>c.Id==id);
            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto,movieInDb);
            _context.SaveChanges();

            return Ok();

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Authorize(Roles = RoleNames.CanManageMovies)]
        public IHttpActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}