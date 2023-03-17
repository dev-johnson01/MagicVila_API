using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVila_WebAPI.Models
{
    public class Vila
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqrft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amaenity { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
