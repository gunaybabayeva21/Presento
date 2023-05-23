using System.ComponentModel.DataAnnotations;

namespace Presento.E.Models
{
    public class Job
    {
        public Job() 
        {
            Teams = new List<Team>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
       public ICollection<Team> Teams { get; set; }
    }
}
