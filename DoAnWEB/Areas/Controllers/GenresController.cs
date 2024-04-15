using System.Diagnostics.Contracts;
using DoAnWEB.Models;
using DoAnWEB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEB.Areas.Controllers
{
	[Authorize(Roles = "Admin")]
	public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetAllAsync();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                    await _genreRepository.AddAsync(genre);
                    return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,Name")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genreRepository.UpdateAsync(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenreExists(genre.GenreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
        private async Task<bool> GenreExists(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre != null;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _genreRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
