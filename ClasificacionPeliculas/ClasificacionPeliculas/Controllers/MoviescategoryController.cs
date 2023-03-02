using ClasificacionPeliculas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClasificacionPeliculas.Controllers
{
    public class MoviescategoryController : Controller
    {
        public IActionResult Index()
        {
            MoviesContext _moviesContext = new MoviesContext();
            IEnumerable<ClasificacionPeliculasModel.Moviescategory> moviescategories =
                (from mc in _moviesContext.Moviescategories
                 join m in _moviesContext.Movies on mc.MovieId equals m.Id
                 join c in _moviesContext.Categories on mc.CategoryId equals c.Id
                 select new ClasificacionPeliculasModel.Moviescategory
                 {
                     Id = mc.Id,
                     MovieName = m.Title,
                     CategoryName = c.Name
                 }).ToList();
            return View(moviescategories);
        }

        public IActionResult Create()
        {
            MoviesContext _moviesContext = new MoviesContext();
            IEnumerable<ClasificacionPeliculasModel.Movie> movies = (from mc in _moviesContext.Movies
                                                                     select new ClasificacionPeliculasModel.Movie
                                                                     {
                                                                         Id = mc.Id,
                                                                         Title = mc.Title,
                                                                     }).ToList();
            IEnumerable<ClasificacionPeliculasModel.Category> categories = (from c in _moviesContext.Categories
                                                                            select new ClasificacionPeliculasModel.Category
                                                                            {
                                                                                Id = c.Id,
                                                                                Name = c.Name
                                                                            }).ToList();
            ClasificacionPeliculasModel.Moviescategory moviescategory = new ClasificacionPeliculasModel.Moviescategory();
            moviescategory.Categories = categories.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            moviescategory.Movies = movies.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Title
            }).ToList();
            return View(moviescategory);
        }
        [HttpPost]
        public IActionResult Create (int MovieId,  int CategoryId)
        {
            Models.Moviescategory moviescategory = new Moviescategory
            {
                MovieId = MovieId,
                CategoryId = CategoryId
            };
            MoviesContext _moviesContext = new MoviesContext();
            _moviesContext.Moviescategories.Add(moviescategory);
            _moviesContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
