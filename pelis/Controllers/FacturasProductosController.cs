using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pelis.Data;
using pelis.Models;

namespace pelis.Controllers
{
    public class FacturasProductosController : Controller
    {
        private readonly pelisContext _context;

        public FacturasProductosController(pelisContext context)
        {
            _context = context;
        }

        // GET: FacturasProductos
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.FacturasProductos == null)
            {
                return Problem("Entity set 'MvcMovieContext.FacturasProductos' is null.");
            }

            var facturasProductos = from fp in _context.FacturasProductos
                                    select fp;

            if (!String.IsNullOrEmpty(searchString))
            {
                // Intentamos convertir searchString a entero para hacer la comparación
                int searchInt;
                bool isNumeric = int.TryParse(searchString, out searchInt);

                if (isNumeric)
                {
                    // Si es un número, comparamos con FacturaId o ProductoId
                    facturasProductos = facturasProductos.Where(fp => fp.FacturaId == searchInt || fp.ProductoId == searchInt);
                }
                else
                {
                    // Si no es numérico, se pueden realizar búsquedas por otros campos, como el nombre del producto
                    // Si quieres buscar por nombre de producto, por ejemplo, puedes hacerlo así:
                    facturasProductos = facturasProductos.Where(fp => fp.Producto.Nombre.Contains(searchString));
                }
            }

            return View(await facturasProductos.ToListAsync());
        }


        // GET: FacturasProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasProductos = await _context.FacturasProductos
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (facturasProductos == null)
            {
                return NotFound();
            }

            return View(facturasProductos);
        }

        // GET: FacturasProductos/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Nombre");  // Cambia "Nombre" por el campo que quieres mostrar de Facturas
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");  // Cambia "Nombre" por el campo que quieres mostrar de Productos
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,ProductoId,Cantidad,Precio,PrecioTotal")] FacturasProductos facturasProductos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturasProductos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facturasProductos);
        }

        // GET: FacturasProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasProductos = await _context.FacturasProductos.FindAsync(id);
            if (facturasProductos == null)
            {
                return NotFound();
            }
            return View(facturasProductos);
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,ProductoId,Cantidad,Precio,PrecioTotal")] FacturasProductos facturasProductos)
        {
            if (id != facturasProductos.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturasProductos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturasProductosExists(facturasProductos.FacturaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(facturasProductos);
        }

        // GET: FacturasProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturasProductos = await _context.FacturasProductos
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (facturasProductos == null)
            {
                return NotFound();
            }

            return View(facturasProductos);
        }

        // POST: FacturasProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturasProductos = await _context.FacturasProductos.FindAsync(id);
            if (facturasProductos != null)
            {
                _context.FacturasProductos.Remove(facturasProductos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturasProductosExists(int id)
        {
            return _context.FacturasProductos.Any(e => e.FacturaId == id);
        }

        [HttpPost]

        public async Task<IActionResult> Crear(FacturasProductos facturasProductos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturasProductos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(facturasProductos);
        }
 
    }
}
