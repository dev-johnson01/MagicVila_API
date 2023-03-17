using MagicVila_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Vila> Vilas { get; set; }
        public DbSet<VilaNumber> VilaNumbers {get; set;}

        protected override void OnModelCreating(ModelBuilder modelbuider)
        {
            modelbuider.Entity<Vila>().HasData(
                new Vila()
                {
                    Id = 1,
                    Name = "Villa pool",
                    Details = "Have fun and enjoy yourself",
                    Sqrft = 100,
                    Occupancy = 4,
                    Rate = 4,
                    Amaenity = "",
                    CreatedDate = DateTime.Now,
                    ImageUrl = ""
                },
                new Vila()
                {
                    Id = 2,
                    Name = "Villa beach",
                    Details = "A place to explore the world",
                    Sqrft = 150,
                    Occupancy = 0,
                    Rate = 5,
                    Amaenity = "",
                    CreatedDate = DateTime.Now,
                    ImageUrl = ""
                }
                ) ;
        }
    }
}
