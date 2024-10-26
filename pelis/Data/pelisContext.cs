using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pelis.Models;

namespace pelis.Data
{
    public class pelisContext : DbContext
    {
        public pelisContext (DbContextOptions<pelisContext> options)
            : base(options)
        {
        }

        public DbSet<pelis.Models.Movie> Movie { get; set; } = default!;
        public DbSet<pelis.Models.Categorias> Categorias { get; set; } = default!;
        public DbSet<pelis.Models.Clientes> Clientes { get; set; } = default!;
        public DbSet<pelis.Models.Facturas> Facturas { get; set; } = default!;
        public DbSet<pelis.Models.Productos> Productos { get; set; } = default!;
        public DbSet<pelis.Models.MediosPago> MediosPago { get; set; } = default!;
        public DbSet<pelis.Models.UsuariosSistema> UsuariosSistema { get; set; } = default!;
        public DbSet<pelis.Models.FacturasProductos> FacturasProductos { get; set; } = default!;
    }
}
