using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schedule_MVC.Data.Models;

namespace Schedule_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Approved> Approved { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Work> Works { get; set; }
    }
}