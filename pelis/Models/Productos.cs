using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pelis.Models
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [StringLength(100)]
        public string Marca { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }

        public int CategoriaId { get; set; }
    }
}
    