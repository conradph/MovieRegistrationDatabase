using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace MovieRegistrationDatabase.Models
{
    public class MovieDAL
    {
        public List<Movie> GetMovies()
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = "select * from movies";
                connect.Open();
                List<Movie> movies = connect.Query<Movie>(sql).ToList();
                connect.Close();
                return movies;
            }
        }
        //read single - take in an id and return the matching row
        public Movie GetMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"select * from movies where id = {id}";
                connect.Open();
                //Query always returns a list regardless of how many movies we want
                //even if our query is meant to only return one movie, we still have to pull it out of its list
                Movie movie = connect.Query<Movie>(sql).First();
                connect.Close();
                return movie;
            }
        }
        public void DeleteMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"delete from movies where id = {id}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        public void UpdateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"update movies " +
                    $"set title = '{m.Title}'" +
                    $",genre = '{m.Genre}'" +
                    $", year = {m.Year}" +
                    $", runtime = {m.Runtime}" +
                    $" where id = {m.ID}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        public void CreateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"insert into movies  " +
                    $"values(0, '{m.Title}', '{m.Genre}', {m.Year}, {m.Runtime})";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        public string CreateSearchString(string option, string search)
        {
            if (option == "id")
            {
                return $"select * from movies where {option} = {search}";
            }
            else
            {
                return $"select * from movies where {option} = '{search}'";
            }
        }
        public List<Movie> SearchMovies(string searchString)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                connect.Open();
                //Query always returns a list regardless of how many movies we want
                //even if our query is meant to only return one movie, we still have to pull it out of its list
                List<Movie> movies = connect.Query<Movie>(searchString).ToList();
                connect.Close();
                return movies;
            }
        }
    }
}
