using Microsoft.AspNetCore.Mvc;
using RunGroopsWebSite.Data;

namespace RunGroopsWebSite.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Races = _context.Races.ToList();
            
            return View(Races);
        }
    }
}
