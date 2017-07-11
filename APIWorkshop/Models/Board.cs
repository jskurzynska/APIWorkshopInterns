using System.ComponentModel.DataAnnotations;

namespace APIWorkshop.Models
{
    public class Board
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; } 
    }
}