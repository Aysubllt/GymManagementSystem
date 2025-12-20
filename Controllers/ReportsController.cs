using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GymManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views/Reports/Index.cshtml dosyasını arar
        }
    }
}