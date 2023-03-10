using RunGroopsWebSite.Data.Enum;
using RunGroopsWebSite.Models;
using System.ComponentModel.DataAnnotations;

namespace RunGroopsWebSite.ViewModel
{
    public class EditRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public string Url { get; set; }
        public IFormFile? Photo { get; set; }
        public RaceCategory Category { get; set; }
    }
}
