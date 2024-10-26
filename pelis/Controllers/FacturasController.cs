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
    public class FacturasController : Controller
    {
        private readonly pelisContext _context;

        public FacturasController(pelisContext context)
        {
            _context = context;
        }

        // GET: Facturas
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


        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Facturador)
                .Include(f => f.MedioPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturas == null)
            {
                return NotFound();
            }

            return View(facturas);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["FacturadorId"] = new SelectList(_context.Set<UsuariosSistema>(), "Id", "Email");
            ViewData["MedioPagoId"] = new SelectList(_context.Set<MediosPago>(), "Id", "Metodo");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Total,ClienteId,MedioPagoId,FacturadorId")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);
            ViewData["FacturadorId"] = new SelectList(_context.Set<UsuariosSistema>(), "Id", "Email", facturas.FacturadorId);
            ViewData["MedioPagoId"] = new SelectList(_context.Set<MediosPago>(), "Id", "Metodo", facturas.MedioPagoId);
            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);
            ViewData["FacturadorId"] = new SelectList(_context.Set<UsuariosSistema>(), "Id", "Email", facturas.FacturadorId);
            ViewData["MedioPagoId"] = new SelectList(_context.Set<MediosPago>(), "Id", "Metodo", facturas.MedioPagoId);
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Total,ClienteId,MedioPagoId,FacturadorId")] Facturas facturas)
        {
            if (id != facturas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturasExists(facturas.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", facturas.ClienteId);
            ViewData["FacturadorId"] = new SelectList(_context.Set<UsuariosSistema>(), "Id", "Email", facturas.FacturadorId);
            ViewData["MedioPagoId"] = new SelectList(_context.Set<MediosPago>(), "Id", "Metodo", facturas.MedioPagoId);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Facturador)
                .Include(f => f.MedioPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturas == null)
            {
                return NotFound();
            }

            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturas = await _context.Facturas.FindAsync(id);
            if (facturas != null)
            {
                _context.Facturas.Remove(facturas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturasExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
