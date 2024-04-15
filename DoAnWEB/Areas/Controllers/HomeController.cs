using System.Diagnostics;
using DoAnWEB.Models;
using DoAnWEB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWEB.Areas.Controllers
{
	[Authorize(Roles = "Admin, Listener")]
	public class HomeController : Controller
    {
		private readonly ISongRepository _songRepository;
		public HomeController(ISongRepository songRepository, IGenreRepository genreRepository)
		{
			_songRepository = songRepository;
		}
		public async Task<IActionResult> Index()
		{
			var songs = await _songRepository.GetAllAsync();
			return View(songs);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

	}
}
