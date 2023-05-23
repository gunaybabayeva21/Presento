using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presento.E.Models;

namespace Presento.E.DataContext
{
    public class PresentoDbContext : IdentityDbContext<AppUser>
    {
       public PresentoDbContext(DbContextOptions<PresentoDbContext> options ):base(options){ }

       public DbSet<Team> Teams { get; set; }
        public DbSet<Job > Jobs { get; set; }


    }
}
