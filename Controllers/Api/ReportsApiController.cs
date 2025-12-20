using GymManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportsApi/daily/2025-12-20
        [HttpGet("daily/{date}")]
        public async Task<IActionResult> GetDailyAppointments(DateTime date)
        {
            // Tek bir sorguda tüm veriyi anonim nesneye projekte ediyoruz (Performanslıdır)
            var report = await _context.Appointments
                .AsNoTracking() // Sadece okuma yaptığı için takip etmeyi kapatıyoruz
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .Where(a => a.AppointmentDate.Date == date.Date)
                .OrderBy(a => a.AppointmentDate)
                .Select(a => new {
                    id = a.Id,
                    trainerName = a.Trainer != null ? a.Trainer.Name : "Eğitmen Yok",
                    serviceName = a.Service != null ? a.Service.Name : "Hizmet Yok",
                    time = a.AppointmentDate.ToString("HH:mm"),
                    isConfirmed = a.IsConfirmed,
                    price = a.Service != null ? a.Service.Price : 0
                })
                .ToListAsync();

            return Ok(report);
        }

        // GET: api/ReportsApi/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetGeneralStats()
        {
            // Çoklu await yerine verileri topluca hesaplıyoruz
            var totalRevenue = await _context.Appointments
                .Where(a => a.IsConfirmed)
                .SumAsync(a => a.Service.Price);

            var totalAppointments = await _context.Appointments.CountAsync();

            var popularService = await _context.Appointments
                .GroupBy(a => a.Service.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                totalRevenue,
                totalAppointments,
                popularService = popularService?.Name ?? "Veri Yok"
            });
        }
    }
}