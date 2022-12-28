using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;
using RunGroopsWebSite.ViewModel;

namespace RunGroopsWebSite.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var Races = await _raceRepository.GetAll();

            return View(Races);
        }

        // make Detail Controller
        public async Task<IActionResult> Detail(int id)
        {
            var race = await _raceRepository.GetRaceByIdAsync(id);

            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                Race race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Address = raceVM.Address,
                };

                if (raceVM.Photo != null)
                {
                    var fileUrl = await _photoService.UploadPhotoAsync(raceVM.Photo);
                    race.Image = fileUrl;
                }

                _raceRepository.Add(race);

                return RedirectToAction("Index");



            }
            else
            {
                return View();
            }
        }
    }
}
