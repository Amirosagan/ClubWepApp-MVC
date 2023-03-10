using Microsoft.AspNetCore.Authorization;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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

        [Authorize(Roles = "admin,user")]

        public IActionResult Create()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel()
            {
                AppUserId = curUser
            };
            return View(createClubViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid)
            {
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Address = clubVM.Address,
                    ClubCategory = clubVM.Category,
                    AppUserId = clubVM.AppUserId
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
        [Authorize(Roles ="admin")]
        public IActionResult Edit(int id)
        {
            Club club = _clubRepository.GetClubById(id);

            EditClubViewModel clubVM = new EditClubViewModel
            {
                Id = club.Id,
                Title = club.Title,
                Description = club.Description,
                Url = club.Image,
                Address = club.Address,
                Category = club.ClubCategory
            };

            return View(clubVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id , EditClubViewModel club)
        {
            if(ModelState.IsValid)
            {
                Club clubToUpdate = _clubRepository.GetClubById(id);

                clubToUpdate.Title = club.Title;
                clubToUpdate.Description = club.Description;
                clubToUpdate.Address = club.Address;
                clubToUpdate.ClubCategory = club.Category;
                Console.WriteLine(club.Url);

                if (club.Photo != null)
                {
                    var fileName = await _photoService.UploadPhotoAsync(club.Photo);
                    clubToUpdate.Image = fileName;
                }

                _clubRepository.Update(clubToUpdate);

                return RedirectToAction("Index");
            }
            else
            {
                return View(club);
            }
            
        }

        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Club club = _clubRepository.GetClubById(id);

            return View(club);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            Club club = _clubRepository.GetClubById(id);

            if (club.Image != null)
            {
                await _photoService.DeletePhotoAsync(club.Image);
            }

            _clubRepository.Remove(club);

            return RedirectToAction("Index");
        }

    }
}
