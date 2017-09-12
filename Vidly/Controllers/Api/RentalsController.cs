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
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(c => newRental.MovieIds.Contains(c.Id));

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");
                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                
                _context.Rentals.Add(rental);
                
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
