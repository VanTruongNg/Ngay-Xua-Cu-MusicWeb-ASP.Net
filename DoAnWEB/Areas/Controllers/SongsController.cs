using DoAnWEB.Models;
using DoAnWEB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEB.Areas.Controllers
{

    [Authorize(Roles = "Admin")]
    public class SongsController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IGenreRepository _genreRepository;

        public SongsController(ISongRepository songRepository, IGenreRepository genreRepository)
        {
            _songRepository = songRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await _songRepository.GetAllAsync();
            return View(songs);
        }

        public async Task<IActionResult> Create()
        {
            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");

            return View();
        }

        public async Task<string> SaveAudio(IFormFile audioFile)
        {
            var savePath = Path.Combine("wwwroot/audio", audioFile.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await audioFile.CopyToAsync(fileStream);
            }
            return "/audio/" + audioFile.FileName;
        }
        public async Task<string> SaveImage(IFormFile img)
        {
            var savePath = Path.Combine("wwwroot/images", img.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }
            return "/images/" + img.FileName;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Song songs, IFormFile imageUrl, IFormFile audioUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    songs.imgFilePath = await SaveImage(imageUrl);
                }
                if (audioUrl != null)
                {
                    songs.mp3FilePath = await SaveAudio(audioUrl);
                }
                await _songRepository.AddAsync(songs);
                return RedirectToAction(nameof(Index));
            }
            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");

            return View(songs);
        }


        public async Task<IActionResult> Details(int id)
        {
            var songs = await _songRepository.GetByIdAsync(id);

            if (songs == null)
            {
                return NotFound();
            }
            return View(songs);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var songs = await _songRepository.GetByIdAsync(id);
            if (songs == null)
            {
                return NotFound();
            }
            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");

            return View(songs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Song song, IFormFile imageUrl, IFormFile audioUrl)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu trữ file âm thanh nếu được chỉnh sửa
                    if (audioUrl != null)
                    {
                        song.mp3FilePath = await SaveAudio(audioUrl);
                    }

                    // Lưu trữ file ảnh nếu được chỉnh sửa
                    if (imageUrl != null)
                    {
                        song.imgFilePath = await SaveImage(imageUrl);
                    }

                    await _songRepository.UpdateAsync(song);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ (nếu có)
                    ModelState.AddModelError("", "Error occurred while updating the song: " + ex.Message);
                }
            }

            // Load lại danh sách thể loại
            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName", song.GenreID);

            return View(song);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var songs = await _songRepository.GetByIdAsync(id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _songRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                // Nếu không có từ khóa tìm kiếm, trả về trang tìm kiếm
                return View();
            }

            var searchResults = await _songRepository.SearchAsync(query);
            return View("Search", searchResults);
        }

    }
}
