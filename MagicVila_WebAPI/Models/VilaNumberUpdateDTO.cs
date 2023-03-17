using System.ComponentModel.DataAnnotations;

namespace MagicVila_WebAPI.Models
{
    public class VilaNumberUpdateDTO
    {
        [Required]
        public int VilaNo { get; set; }
        [Required]
        public int VilaID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
