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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Movie model)
        {
            if (ModelState.IsValid)
            {
                List<Movie> movies = new List<Movie>();
                using (var connect = new MySqlConnection(Secret.connection))
                {
                    string insertQuery = @"INSERT INTO movies VALUES (@ID, @Title, @Genre, @Year, @Runtime);";
                    connect.Open();
                    var result = connect.Execute(insertQuery, model);
                    string selectQuery = "select * from movies";
                    movies = connect.Query<Movie>(selectQuery).ToList();
                    connect.Close();
                }
                return RedirectToAction("List");
            }
            return View(model);
        }
        public IActionResult List()
        {
            List<Movie> movies = new List<Movie>();
            using (var connect = new MySqlConnection(Secret.connection))
            {
                string selectQuery = "select * from movies";
                movies = connect.Query<Movie>(selectQuery).ToList();
                connect.Close();
            }
            return View(movies);
        }
        
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string option, string search)
        {
            List<Movie> movies = new List<Movie>();
            
            string selectQuery = $"select * from movies where {option} = '{search}'";
            return RedirectToAction("SearchResults", new { selectString = selectQuery });
        }
        public IActionResult SearchResults(string selectString)
        {
            List<Movie> movies = new List<Movie>();
            using (var connect = new MySqlConnection(Secret.connection))
            {
                movies = connect.Query<Movie>(selectString).ToList();
                connect.Close();
            }
            return View(movies);
        }
    }
}
