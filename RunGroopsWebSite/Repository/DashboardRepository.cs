using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var carUser =  _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs =  _context.Clubs.Where(ele => ele.AppUser.Id.Equals(carUser)).ToList();
            return userClubs;
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var caruser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(ele => ele.AppUser.Id.Equals(caruser)).ToList();

            return userRaces;
        }
    }
}
