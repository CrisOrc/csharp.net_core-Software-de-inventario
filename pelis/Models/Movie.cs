using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pelis.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Display(Name = "Fecha Lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
