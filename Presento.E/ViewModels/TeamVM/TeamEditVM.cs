using Presento.E.Models;

namespace Presento.E.ViewModels.TeamVM
{
    public class TeamEditVM
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Description { get; set; }
        public string? ProfilImageName { get; set; } 
        public int JobId { get; set; }
        public IFormFile? Image { get; set; }
        public List<Job>? Jobs { get; set; }
    }
}
