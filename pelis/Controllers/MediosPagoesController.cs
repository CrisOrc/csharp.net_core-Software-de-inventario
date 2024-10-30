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
    public class MediosPagoesController : Controller
    {
        private readonly pelisContext _context;

        public MediosPagoesController(pelisContext context)
        {
            _context = context;
        }

        // GET: MediosPagoes
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.MediosPago == null)
            {
                return Problem("Entity set 'MvcMovieContext.MediosPago' is null.");
            }

            var mediosPago = from m in _context.MediosPago
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mediosPago = mediosPago.Where(m => m.Metodo!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await mediosPago.ToListAsync());
        }


        // GET: MediosPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.MediosPago
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediosPago == null)
            {
                return NotFound();
            }

            return View(mediosPago);
        }

        // GET: MediosPagoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediosPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Metodo")] MediosPago mediosPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediosPago);
        }

        // GET: MediosPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.MediosPago.FindAsync(id);
            if (mediosPago == null)
            {
                return NotFound();
            }
            return View(mediosPago);
        }

        // POST: MediosPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Metodo")] MediosPago mediosPago)
        {
            if (id != mediosPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediosPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediosPagoExists(mediosPago.Id))
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
            return View(mediosPago);
        }

        // GET: MediosPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.MediosPago
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediosPago == null)
            {
                return NotFound();
            }

            return View(mediosPago);
        }

        // POST: MediosPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediosPago = await _context.MediosPago.FindAsync(id);
            if (mediosPago != null)
            {
                _context.MediosPago.Remove(mediosPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediosPagoExists(int id)
        {
            return _context.MediosPago.Any(e => e.Id == id);
        }
    }
}
