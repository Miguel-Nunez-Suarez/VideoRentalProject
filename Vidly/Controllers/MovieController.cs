using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //  movies/
        public ActionResult Index()
        {
            if (User.IsInRole(RoleNames.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        public ActionResult details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }

        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                genre = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult Edit (int id)
        {
            var movie = _context.Movies.SingleOrDefault(c=>c.Id==id);
            if (movie == null)
                return new HttpNotFoundResult();

            var viewModel = new MovieFormViewModel
            {
                movie = movie,
                genre = _context.Genres.ToList()
            };
            return View("MovieForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    movie = movie,
                    genre = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            else
            { 
                if (movie.Id==0)
                    _context.Movies.Add(movie);
                else
                {
                    var movieInDb = _context.Movies.Single(c=>c.Id==movie.Id);
                    movieInDb.Name = movie.Name;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.NumberInStock = movie.NumberInStock;
                }

                _context.SaveChanges();
                            
                return RedirectToAction("Index","Movie");
            }
        }
    }
}