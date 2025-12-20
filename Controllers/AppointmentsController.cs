using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ADMIN: Tüm randevuları listele
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .Include(a => a.Member)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
            return View(appointments);
        }

        // ÜYE: Kendi randevularını listele
        public async Task<IActionResult> MyAppointments()
        {
            var userId = _userManager.GetUserId(User);
            var appointments = await _context.Appointments
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .Where(a => a.MemberId == userId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
            return View(appointments);
        }

        // ÜYE: Randevu Alma (GET)
        public IActionResult Book()
        {
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book([Bind("TrainerId,ServiceId,AppointmentDate")] Appointment appointment)
        {
            // Model hatalarını temizle (Sistem tarafından otomatik eklenen zorunlu alanlar için)
            ModelState.Remove("MemberId");
            ModelState.Remove("Member");
            ModelState.Remove("Trainer");
            ModelState.Remove("Service");

            var trainer = await _context.Trainers.Include(t => t.Gym).FirstOrDefaultAsync(t => t.Id == appointment.TrainerId);
            var service = await _context.Services.FindAsync(appointment.ServiceId);

            if (trainer == null || service == null || trainer.Gym == null)
            {
                ModelState.AddModelError("", "Seçilen antrenör, hizmet veya salon bilgisi eksik.");
            }
            else
            {
                // 1. Tarih Kontrolü
                if (appointment.AppointmentDate < DateTime.Now)
                    ModelState.AddModelError("", "Geçmiş bir tarihe randevu alamazsınız.");

                // 2. Çalışma Günü Kontrolü
                DayOfWeek day = appointment.AppointmentDate.DayOfWeek;
                bool isGymOpen = day switch
                {
                    DayOfWeek.Monday => trainer.Gym.OpenMonday,
                    DayOfWeek.Tuesday => trainer.Gym.OpenTuesday,
                    DayOfWeek.Wednesday => trainer.Gym.OpenWednesday,
                    DayOfWeek.Thursday => trainer.Gym.OpenThursday,
                    DayOfWeek.Friday => trainer.Gym.OpenFriday,
                    DayOfWeek.Saturday => trainer.Gym.OpenSaturday,
                    DayOfWeek.Sunday => trainer.Gym.OpenSunday,
                    _ => false
                };

                bool isTrainerWorking = day switch
                {
                    DayOfWeek.Monday => trainer.WorkMonday,
                    DayOfWeek.Tuesday => trainer.WorkTuesday,
                    DayOfWeek.Wednesday => trainer.WorkWednesday,
                    DayOfWeek.Thursday => trainer.WorkThursday,
                    DayOfWeek.Friday => trainer.WorkFriday,
                    DayOfWeek.Saturday => trainer.WorkSaturday,
                    DayOfWeek.Sunday => trainer.WorkSunday,
                    _ => false
                };

                if (!isGymOpen) ModelState.AddModelError("", "Spor salonu seçilen günde kapalıdır.");
                if (!isTrainerWorking) ModelState.AddModelError("", "Antrenör seçilen günde çalışmamaktadır.");

                // 3. Saat Aralığı Kontrolü
                TimeSpan appStart = appointment.AppointmentDate.TimeOfDay;
                TimeSpan appEnd = appStart.Add(TimeSpan.FromMinutes(service.DurationMinutes));

                if (appStart < trainer.Gym.OpeningTime || appEnd > trainer.Gym.ClosingTime)
                    ModelState.AddModelError("", $"Salon saatleri dışındasınız: {trainer.Gym.OpeningTime:hh\\:mm} - {trainer.Gym.ClosingTime:hh\\:mm}");

                if (appStart < trainer.ShiftStartTime || appEnd > trainer.ShiftEndTime)
                    ModelState.AddModelError("", $"Antrenör mesai saatleri dışındasınız: {trainer.ShiftStartTime:hh\\:mm} - {trainer.ShiftEndTime:hh\\:mm}");

                // 4. Çakışma Kontrolü (Hata Veren Kısım Düzeltildi)
                if (ModelState.IsValid)
                {
                    // Verileri önce listeye çekiyoruz (In-Memory), sonra çakışma kontrolü yapıyoruz
                    var existingApps = await _context.Appointments
                        .Where(a => a.TrainerId == appointment.TrainerId && a.IsConfirmed && a.AppointmentDate.Date == appointment.AppointmentDate.Date)
                        .Include(a => a.Service)
                        .ToListAsync();

                    bool isConflict = existingApps.Any(a => {
                        var exStart = a.AppointmentDate.TimeOfDay;
                        var exEnd = exStart.Add(TimeSpan.FromMinutes(a.Service.DurationMinutes));
                        return (appStart < exEnd && appEnd > exStart);
                    });

                    if (isConflict) ModelState.AddModelError("", "Bu saat diliminde antrenörün başka bir randevusu var.");
                }
            }

            if (ModelState.IsValid)
            {
                appointment.MemberId = _userManager.GetUserId(User);
                appointment.IsConfirmed = false;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Randevu talebiniz iletildi. Onay bekliyor.";
                return RedirectToAction(nameof(MyAppointments));
            }

            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Name", appointment.TrainerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.IsConfirmed = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Randevu onaylandı.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null && (User.IsInRole("Admin") || appointment.MemberId == _userManager.GetUserId(User)))
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                TempData["Info"] = "Randevu iptal edildi.";
            }
            return User.IsInRole("Admin") ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(MyAppointments));
        }
    }
}