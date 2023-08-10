using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.Parametro;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.Usuario;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Usuario;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionLibreria")));
builder.Services.AddScoped<IRepositorioTipoCabania, RepositorioTipoCabania>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioCabania, RepositorioCabania>();
builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddScoped<IRepositorioParametros, RepositorioParametro>();


//--------------------Casos de Usos----------------------------//
builder.Services.AddScoped<IAltaTipoCabania, AltaTipoCabania>();
builder.Services.AddScoped<IGetTiposCabania, GetTiposCabania>();
builder.Services.AddScoped<IGetByNameTipoCabania, GetByNameTipoCabania>();
builder.Services.AddScoped<IRemoveByIdTipoCabania, RemoveByIdTipoCabania>();
builder.Services.AddScoped<IUpdateTipoCabania, UpdateTipoCabania>();

//*******************************************************************//
builder.Services.AddScoped<IGetParametro, GetParametro>();

//*************************************************************//
builder.Services.AddScoped<IFindByNameYPassUsuario, FindByNameYPassUsuario>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
