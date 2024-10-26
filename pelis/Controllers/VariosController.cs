using Microsoft.AspNetCore.Mvc;

namespace pelis.Controllers
{
    public class VariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /*public IActionResult Suma()
        {            
            return View();
        }*/

        [HttpPost]
        public IActionResult Suma(int num1, int num2)
        {
            int resultado = num1 + num2;
            ViewBag.Num1 = num1;
            ViewBag.Num2 = num2;
            ViewBag.resultado = resultado;
            return View();
        }

        [HttpGet]
        public IActionResult Suma()
        {            
            return View();
        }
    }
}
