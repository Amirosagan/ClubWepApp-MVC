using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.ViewModel;

namespace RunGroopsWebSite.Controllers
{
    [Authorize(Roles = "admin,user")]
    
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            var userRaces = _dashboardRepository.GetAllUserRaces();
            var userClubs = _dashboardRepository.GetAllUserClubs();
            DashboardViewModel model = new DashboardViewModel()
            { 
                Races = userRaces.Result,
                Clubs = userClubs.Result
            };

            return View(model);
        }
    }
}
