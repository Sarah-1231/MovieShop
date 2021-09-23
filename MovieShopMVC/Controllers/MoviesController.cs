using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController: Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Details(int id)
        {
            return View();
        }

        //public IActionResult GetTopRevenueMovies()
        //{
        //    var movies = _movieService.Get30HighestGrossingMovies();
        //    return View(movies);
        //}
    }
}
