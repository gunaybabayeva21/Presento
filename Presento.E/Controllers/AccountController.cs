using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presento.E.Models;
using Presento.E.ViewModels.AccountVM;

namespace Presento.E.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) { return View(); }

            AppUser appUser = new AppUser()
            {
                Name = register.Name,
                Email = register.Email,
                Surname = register.Surname,
                UserName = register.UserName,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser);
            if(!result.Succeeded) 
            { 
              foreach(IdentityError? item in result.Errors)
              {
                    ModelState.AddModelError("", item.Description);
              }return View();   
            }return RedirectToAction("Login");
        }
    }


}

