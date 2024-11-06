using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using pelis.Data;


namespace pelis.Controllers
{
    public class ReportController : Controller
    {
        private readonly IConverter _converter;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly pelisContext _context;

        public ReportController(IConverter converter, ICompositeViewEngine viewEngine, pelisContext context)
        {
            _converter = converter;
            _viewEngine = viewEngine;
            _context = context; // Almacena el contexto para usarlo en la base de datos
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePdf()
        {
            // Obtener productos de la base de datos
            var productos = await _context.Productos.ToListAsync();

            // Generar HTML para el reporte
            var htmlContent = await RenderViewToString("ReportTemplate", productos);

            // Configuración del PDF
            var pdfDoc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings { Top = 10, Bottom = 10 }
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            var pdf = _converter.Convert(pdfDoc);

            return File(pdf, "application/pdf", "ReporteProductos.pdf");
        }

        private async Task<string> RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} no fue encontrado.");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
