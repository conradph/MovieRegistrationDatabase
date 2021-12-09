using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieRegistrationDatabase.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRegistrationDatabase.Controllers
{
    public class MovieController : Controller
    {
        public MovieDAL MovieDB = new MovieDAL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Movie model)
        {
            if (ModelState.IsValid)
            {
                MovieDB.CreateMovie(model);
                return RedirectToAction("List");
            }
            return View(model);
        }
        public IActionResult List()
        {
            List<Movie> movies = MovieDB.GetMovies();
            return View(movies);
        }

        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string option, string search)
        {
            return RedirectToAction("SearchResults", new { selectString = MovieDB.CreateSearchString(option, search) });
        }
        public IActionResult SearchResults(string selectString)
        {
            List<Movie> movies = MovieDB.SearchMovies(selectString);
            return View(movies);
        }
        public IActionResult Edit(int id)
        {
            Movie m = MovieDB.GetMovie(id);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return RedirectToAction("IDError", id);
            }
        }
        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("List");
            }
            return View(m);
        }
        public IActionResult Details(int id)
        {
            Movie m = MovieDB.GetMovie(id);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return RedirectToAction("IDError", id);
            }
        }
        public IActionResult Delete(int id)
        {
            Movie m = MovieDB.GetMovie(id);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return RedirectToAction("IDError", id);
            }
        }
        public IActionResult DeleteFromDB(int id)
        {
            MovieDB.DeleteMovie(id);
            return RedirectToAction("List");
        }
    }
}
