using Microsoft.EntityFrameworkCore;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Club club)
        {
            _context.Clubs.Add(club);
            return Save();
        }

        public bool Remove(Club club)
        {
            _context.Clubs.Remove(club);
            return Save();
        }

        public bool Update(Club club)
        {
            _context.Clubs.Update(club);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).Include(ele=>ele.Address).ToListAsync();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
            return await _context.Clubs.Include(ele=> ele.Address).FirstOrDefaultAsync(ele => ele.Id == id);
        }

        public Club GetClubById(int id)
        {
            return _context.Clubs.Include(ele => ele.Address).FirstOrDefault(ele => ele.Id == id);
        }
    
        
    }
}
