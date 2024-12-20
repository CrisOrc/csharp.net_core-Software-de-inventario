﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pelis.Models
{
    public class FacturasProductos
    {
        [Key, Column(Order = 0)]

        [ForeignKey("Facturas")]
        public int FacturaId { get; set; }
        public Facturas Factura { get; set; }

        [ForeignKey("Productos")]
        public int ProductoId { get; set; }
        public virtual Productos Producto { get; set; }


        public int Cantidad { get; set; }

        public double Precio { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double PrecioTotal { get; set; } // Columna calculada
    }
}
