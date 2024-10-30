using System.ComponentModel.DataAnnotations;

namespace pelis.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }
    }
}
