using Microsoft.AspNetCore.Mvc;
using RunGroopsWebSite.Models;

namespace RunGroopsWebSite.ViewModel
{
    public class DashboardViewModel 
    {
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }
    }
}
