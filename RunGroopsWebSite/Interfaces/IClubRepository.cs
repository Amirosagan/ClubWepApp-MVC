using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();

        Task<IEnumerable<Club>> GetClubByCity(string city);

        Task<Club> GetClubByIdAsync(int id);

        Club GetClubById(int id);

        bool Add(Club club);

        bool Remove(Club club);

        bool Update(Club club);

        bool Save();
    }
}
