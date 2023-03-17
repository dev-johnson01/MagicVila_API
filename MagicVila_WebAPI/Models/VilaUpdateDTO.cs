using System.ComponentModel.DataAnnotations;

namespace MagicVila_WebAPI.Models
{
    public class VilaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public string Details { get; set; }
        [Required]
        public int Sqrft { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amaenity { get; set; }
    }
}
