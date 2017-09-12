using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie movie { get; set; }
        public List<Genre> genre { get; set; }
    }
}