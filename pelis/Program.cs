using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pelis.Data;
using DinkToPdf;
using DinkToPdf.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<pelisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pelisContext") ?? throw new InvalidOperationException("Connection string 'pelisContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.Run();
