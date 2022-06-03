using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        List<Movie> ListaMovies = new List<Movie>();

        public void PrendiDati()
        {
            Movie PrimoMovie = new Movie()
            {
                Id = 1,
                Title = "WRC",
                ReleaseDate = DateTime.Parse("1991 - 4 - 17"),
                Genre = "Adrenaline",
                Price = 4.99M,
                Photo = "/Img/fondo-pag-speciali.jpg",
                AlternateText = "Pranaya Rout Photo not available"
            };
            ListaMovies.Add(PrimoMovie);

            Movie SecondoMovie = new Movie()
            {
                Id = 2,
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989 - 2 - 12"),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Photo = "/Img/fondo-pag-speciali.jpg",
                AlternateText = "Pranaya Rout Photo not available"
            };
            ListaMovies.Add(SecondoMovie);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMovie()
        {
            PrendiDati();
            return View(ListaMovies);
        }

        public string ProvaParametri(string Nome, string Cognome)
        {
            string sAppo = "I dati inseriti sono: " + Nome + " e " + Cognome;
            string sQueryString = Request.QueryString.ToString();
            return sAppo;
        }
    }
}
