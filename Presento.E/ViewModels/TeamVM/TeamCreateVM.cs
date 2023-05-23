using Presento.E.Models;
using System.ComponentModel.DataAnnotations;

namespace Presento.E.ViewModels.TeamVM
{
    public class TeamCreateVM
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Description { get; set; }= null!;
        public int JobId { get ; set; }
        public IFormFile Image { get; set; } = null!;
        public List<Job>? Jobs { get; set; } 

    }
}
