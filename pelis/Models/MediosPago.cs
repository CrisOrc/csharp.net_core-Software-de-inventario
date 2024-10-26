using System.ComponentModel.DataAnnotations;

namespace pelis.Models
{
    public class MediosPago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Metodo { get; set; }
    }
}
