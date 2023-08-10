using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using Libreria.LogicaAplicacion.InterfacesCasoUso;
using Libreria.LogicaAplicacion.ImplementacionCasoUso;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionLibreria")));
builder.Services.AddScoped<IRepositorioTipoCabania, RepositorioTipoCabania>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioCabania, RepositorioCabania>();
builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddScoped<IRepositorioParametros, RepositorioParametro>();


//--------------------
builder.Services.AddScoped<IAltaTipoCabania, AltaTipoCabania>();
builder.Services.AddScoped<IGetTiposCabania, GetTiposCabania>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
