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
    public class UsuariosSistemasController : Controller
    {
        private readonly pelisContext _context;

        public UsuariosSistemasController(pelisContext context)
        {
            _context = context;
        }

        // GET: UsuariosSistemas
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


        // GET: UsuariosSistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }

            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuariosSistemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Email,PasswordHash,Rol")] UsuariosSistema usuariosSistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosSistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema.FindAsync(id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }
            return View(usuariosSistema);
        }

        // POST: UsuariosSistemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,PasswordHash,Rol")] UsuariosSistema usuariosSistema)
        {
            if (id != usuariosSistema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosSistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosSistemaExists(usuariosSistema.Id))
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
            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }

            return View(usuariosSistema);
        }

        // POST: UsuariosSistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuariosSistema = await _context.UsuariosSistema.FindAsync(id);
            if (usuariosSistema != null)
            {
                _context.UsuariosSistema.Remove(usuariosSistema);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosSistemaExists(int id)
        {
            return _context.UsuariosSistema.Any(e => e.Id == id);
        }
    }
}
