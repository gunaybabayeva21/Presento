using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presento.E.DataContext;
using Presento.E.Models;

namespace Presento.E.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController:Controller
    {
        public IActionResult Index() { return View(); } 
        

    }
}
