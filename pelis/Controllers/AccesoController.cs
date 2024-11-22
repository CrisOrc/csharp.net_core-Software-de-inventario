using Microsoft.AspNetCore.Mvc;
using pelis.Models;
using pelis.Data;

namespace pelis.Controllers
{
    public class AccesoController : Controller
    {
        private readonly pelisContext _context;

        public AccesoController(pelisContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuariosSistema usuariosSistema)
        {
            Logic logic_usuario = new Logic(_context);
            var usuario = logic_usuario.ValidarUsuario(usuariosSistema.Username,usuariosSistema.PasswordHash);
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
