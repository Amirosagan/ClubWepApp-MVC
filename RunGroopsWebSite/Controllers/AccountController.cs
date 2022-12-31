using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopsWebSite.Data;
using RunGroopsWebSite.Models;
using RunGroopsWebSite.ViewModel;

namespace RunGroopsWebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet, ActionName("Login")]

        
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl)
        {
            var response = new LoginViewModel();

            if (ReturnUrl != null)
            {
                TempData["Error"] = "Please Login to continue";
            }

            return View(response);
        }

        
        
        [HttpPost]
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.FindByEmailAsync(model.Email);

                if(result is not null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(result, model.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Invalid Login Attempt";
            }

            return View(model);
        }

        [HttpGet, ActionName("Register")]

        public IActionResult Register()
        {
            var response = new RegisterViewModel();

            return View(response);
        }

        [HttpPost, ActionName("Register")]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    TempData["Error"] = "Email already in use";
                    model.Password = "";
                    return View(model);
                }
                var newUser = new AppUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var response = await _userManager.CreateAsync(newUser, model.Password);
                if(response.Succeeded)
                {
                    Console.WriteLine("All Good");
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["Error"] = "Should Bass Conations numbers and letters and nonAlfaletters"; 
                }
                
            }

            model.Password = "";
            return View(model);
        }

        [HttpGet, ActionName("Logout")]
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
