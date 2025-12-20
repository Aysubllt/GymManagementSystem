using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.Controllers
{
    [Authorize] // Giriş zorunlu
    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TrainersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // HERKES: Antrenörleri listele (Hangi salonda olduğunu Include ile alıyoruz)
        public async Task<IActionResult> Index()
        {
            var trainers = _context.Trainers.Include(t => t.Gym);
            return View(await trainers.ToListAsync());
        }

        // HERKES: Antrenör detayları ve randevu saatlerini gör
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // --- SADECE ADMIN İŞLEMLERİ ---

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.GymId = new SelectList(_context.Gyms, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specialty,Bio,GymId,ShiftStartTime,ShiftEndTime,WorkMonday,WorkTuesday,WorkWednesday,WorkThursday,WorkFriday,WorkSaturday,WorkSunday")] Trainer trainer, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null) trainer.ImagePath = await SaveImage(ImageFile);
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GymId = new SelectList(_context.Gyms, "Id", "Name", trainer.GymId);
            return View(trainer);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return NotFound();

            ViewBag.GymId = new SelectList(_context.Gyms, "Id", "Name", trainer.GymId);
            return View(trainer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        // Buraya Bind listesini ekledik ki güncellenen saatler ve günler veritabanına yazılabilsin
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specialty,Bio,GymId,ImagePath,ShiftStartTime,ShiftEndTime,WorkMonday,WorkTuesday,WorkWednesday,WorkThursday,WorkFriday,WorkSaturday,WorkSunday")] Trainer trainer, IFormFile? ImageFile)
        {
            if (id != trainer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null)
                    {
                        if (!string.IsNullOrEmpty(trainer.ImagePath)) DeleteImage(trainer.ImagePath);
                        trainer.ImagePath = await SaveImage(ImageFile);
                    }

                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GymId = new SelectList(_context.Gyms, "Id", "Name", trainer.GymId);
            return View(trainer);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trainer == null) return NotFound();
            return View(trainer);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                if (!string.IsNullOrEmpty(trainer.ImagePath)) DeleteImage(trainer.ImagePath);
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- YARDIMCI METOTLAR (Helper Methods) ---

        private async Task<string> SaveImage(IFormFile file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(wwwRootPath, "images", "trainers");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/images/trainers/" + fileName;
        }

        private void DeleteImage(string imagePath)
        {
            string fullPath = Path.Combine(_hostEnvironment.WebRootPath, imagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
        }

        private bool TrainerExists(int id) => _context.Trainers.Any(e => e.Id == id);
    }
}