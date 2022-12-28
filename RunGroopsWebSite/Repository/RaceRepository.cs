using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            _context.Races.Add(race);
            
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(ele => ele.Address.City.Contains(city)).Include(ele=>ele.Address).ToListAsync();
        }

        public Race GetRaceById(int id)
        {
            return _context.Races.Include(ele=>ele.Address).FirstOrDefault(el=>el.Equals(id));
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            return await _context.Races.Include(ele => ele.Address).FirstOrDefaultAsync(ele => ele.Id == id);
        }

        public bool Remove(Race race)
        {
            _context.Races.Remove(race);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Races.Update(race);

            return Save();
        }
    }
}
