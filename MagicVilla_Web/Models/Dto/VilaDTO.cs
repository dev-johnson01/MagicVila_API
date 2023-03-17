using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VilaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public string ? Details { get; set; }
        public int Sqrft { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ? ImageUrl { get; set; }
        public string Amaenity { get; set; }
    }
}
