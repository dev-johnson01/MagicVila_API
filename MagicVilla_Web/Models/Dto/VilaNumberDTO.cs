using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_Web.Models.Dto
{
    public class VilaNumberDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VilaNo { get; set; }
        [Required]
        public int VilaID { get; set; }
        public string SpecialDetails { get; set; }
        public VilaDTO Vila { get; set; }
    }
}
