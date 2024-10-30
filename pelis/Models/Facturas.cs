using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pelis.Models
{
    public class Facturas
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public double Total { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; } // Navegación a Cliente

        [ForeignKey("MedioPago")]
        public int MedioPagoId { get; set; }
        public MediosPago MedioPago { get; set; } // Navegación a MedioPago

        [ForeignKey("Facturador")]
        public int FacturadorId { get; set; }
        public UsuariosSistema Facturador { get; set; } // Navegación a UsuariosSistema (Facturador)
    }
}
