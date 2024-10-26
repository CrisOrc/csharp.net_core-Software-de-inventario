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
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await movies.ToListAsync());
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
            return View();
        }

        // POST: FacturasProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: FacturasProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}
