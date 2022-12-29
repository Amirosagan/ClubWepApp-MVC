using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.Models;
using RunGroopsWebSite.Repository;
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

        public async Task<IActionResult> Edit(int id )
        {
            Race race = await _raceRepository.GetRaceByIdAsync(id);

            EditRaceViewModel raceVM = new EditRaceViewModel
            {
                Address = race.Address,
                Url = race.Image,
                Title = race.Title,
                Description = race.Description,
                Category = race.RaceCategory,
                Id = race.Id
            };

            return View(raceVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id , EditRaceViewModel race)
        {
            if (ModelState.IsValid)
            {
                Race raceToUpdate = await _raceRepository.GetRaceByIdAsync(id);

                raceToUpdate.Title = race.Title;
                raceToUpdate.Description = race.Description;
                raceToUpdate.Address = race.Address;
                raceToUpdate.RaceCategory = race.Category;
                Console.WriteLine(race.Url);

                if (race.Photo != null)
                {
                    var fileName = await _photoService.UploadPhotoAsync(race.Photo);
                    raceToUpdate.Image = fileName;
                }

                _raceRepository.Update(raceToUpdate);

                return RedirectToAction("Index");
            }
            else
            {
                return View(race);
            }
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            Race race = await _raceRepository.GetRaceByIdAsync(id);

            return View(race);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Race race = await _raceRepository.GetRaceByIdAsync(id);

            if(race.Image is not null)
            {
                _photoService.DeletePhotoAsync(race.Image);
            }

            _raceRepository.Remove(race);

            return RedirectToAction("Index");

        }
    }
}
