using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Data.Enum;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;
using RunGroopsWebSite.ViewModel;

namespace RunGroopsWebSite.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
        }
        
        
        
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubRepository.GetAll();

            return View(clubs); 
            
        }

        public async Task<IActionResult> Detail(int id)
        {
            var club = await _clubRepository.GetClubByIdAsync(id);

            return View(club);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid)
            {
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Address = clubVM.Address,
                    ClubCategory = clubVM.Category
                };

                if (clubVM.Photo != null)
                {
                    var fileName = await _photoService.UploadPhotoAsync(clubVM.Photo);
                    club.Image = fileName;
                }

                _clubRepository.Add(club);

                return RedirectToAction("Index");
            }else
            {
                return View(clubVM);
            }
            
        }
    }
}
