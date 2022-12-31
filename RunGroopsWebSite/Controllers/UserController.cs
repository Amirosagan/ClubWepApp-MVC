using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroopsWebSite.Interfaces;
using RunGroopsWebSite.ViewModel;

namespace RunGroopsWebSite.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> usersVM = new List<UserViewModel>();

            foreach (var user in users)
            {
                usersVM.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Pace = user.Pace,
                    Mileage = user.Mileage
                    
                });
            }
            
            return View(usersVM);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetUserById(id);
            _userRepository.Delete(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userVM = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Pace = user.Pace,
                Mileage = user.Mileage
            };

            return View(userVM);
        }
    }
}
