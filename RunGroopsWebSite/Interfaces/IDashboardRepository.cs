using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
    }
}
