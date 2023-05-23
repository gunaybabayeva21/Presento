using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presento.E.DataContext;
using Presento.E.Models;
using Presento.E.ViewModels.TeamVM;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Presento.E.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly PresentoDbContext _presentoDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public TeamController(PresentoDbContext presentoDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _presentoDbContext = presentoDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _presentoDbContext.Teams.ToListAsync();
            return View(teams);
        }


        public async Task<IActionResult> Create()
        {
            TeamCreateVM teamvm = new TeamCreateVM()
            {
                Jobs = await _presentoDbContext.Jobs.ToListAsync()
            };
            return View(teamvm);
        }

        public async Task<IActionResult> Create(TeamCreateVM newTeam)
        {
            if (!ModelState.IsValid)
            {
                newTeam.Jobs = await _presentoDbContext.Jobs.ToListAsync();
                return (View(newTeam));
            };

            Team team = new Team()
            {
                Name = newTeam.Name,
                Surname = newTeam.Surname,
                Description = newTeam.Description,
                JobId = newTeam.JobId,
            };
            if (newTeam.Image.ContentType.Contains("Images/") && newTeam.Image.Length / 1024 > 2048)
            {
                newTeam.Jobs = await _presentoDbContext.Jobs.ToListAsync();
                ModelState.AddModelError("", "Images");
                return View(newTeam);
            }
            string newFileName = Guid.NewGuid().ToString() + newTeam.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "testimonials", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                newTeam.Image.CopyTo(stream);
            }
            team.ProfilImageName = newFileName;
            await _presentoDbContext.AddAsync(newTeam);
            await _presentoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Team? team = await _presentoDbContext.Teams.FindAsync(id);
            TeamEditVM teamEditVM = new TeamEditVM()
            {
                ProfilImageName = team.ProfilImageName,
                Description = team.Description,
                Surname = team.Surname,
                JobId = team.JobId,
                Name = team.Name,

            };
            return View(teamEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamEditVM newTeam, int id)
        {
            Team? team = await _presentoDbContext.Teams.FindAsync(id);
            if (team == null) { return NotFound(); }
            if (!ModelState.IsValid)
            {
                newTeam.Jobs = await _presentoDbContext.Jobs.ToListAsync();
                return (View(newTeam));
            };

            if (newTeam.Image == null!)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "testimonials", team.ProfilImageName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                string newFileName = Guid.NewGuid().ToString() + newTeam.Image.FileName;
                using(FileStream stream= new FileStream(path, FileMode.CreateNew))
                {
                    newTeam.Image.CopyTo(stream);
                }
                team.ProfilImageName = newFileName;
            }
            team.Surname = newTeam.Name;
            team.Name = newTeam.Name;
            team.Description = newTeam.Description;
            team.JobId= newTeam.JobId;

            _presentoDbContext.SaveChanges();
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult>Delete(int id)
        {
           Team? team= await _presentoDbContext.Teams.FindAsync(id);
            if(team == null) { return NotFound(); }

            string path = Path.Combine(_webHostEnvironment.WebRootPath, " assets", "img", "testimonials", team.ProfilImageName);
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            };

            _presentoDbContext.Teams.Remove(team);
            await _presentoDbContext.SaveChangesAsync() ;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Details(int id)
        {
            Team? team= await _presentoDbContext.Teams.FirstAsync(x => x.Id == id);
            if (team == null) { return NotFound(); };
            return View(team);
        }
    }
}
