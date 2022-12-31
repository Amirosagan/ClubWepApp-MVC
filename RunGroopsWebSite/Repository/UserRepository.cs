using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AppUser user)
        {
            _context.Users.Add(user);
            return this.Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Users.Remove(user);

            return this.Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            var Users = await _context.Users.ToListAsync();

            return Users;
        }

        public async  Task<AppUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));

            return user;
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0) ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return this.Save();
        }
    }
}
