using RunGroopsWebSite.Data.Enum;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.ViewModel
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Photo { get; set; }
        public ClubCategory Category { get; set; }
        public string AppUserId { get; set; }
    }
}
