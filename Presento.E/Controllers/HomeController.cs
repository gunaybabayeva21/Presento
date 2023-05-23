using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presento.E.DataContext;
using Presento.E.Models;
using Presento.E.ViewModel;
using System.Diagnostics;

namespace Presento.E.Controllers
{
    public class HomeController : Controller
    {
        private readonly PresentoDbContext _presentoDbContext;

        public HomeController(PresentoDbContext presentoDbContext)
        {
            _presentoDbContext = presentoDbContext;
        }
            
        public async Task<IActionResult> Index()
        {
            List<Team>teams=await _presentoDbContext.Teams.Include(c=>c.Job).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Teams = teams
            };
            return View(homeVM);
        } 

       
        
    }

}