using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();

        Task<IEnumerable<Race>> GetRaceByCity(string city);

        Task<Race> GetRaceByIdAsync(int id);

        Race GetRaceById(int id);

        bool Add(Race race);

        bool Remove(Race race);

        bool Update(Race race);

        bool Save();

    }
}
