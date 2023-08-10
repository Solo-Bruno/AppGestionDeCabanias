using Librería.LogicaAccesoDatos.EF;
using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.CabaniasCar;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.Mantenimiento;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.Parametro;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.TipoCab;
using Libreria.LogicaAplicacion.ImplementacionCasoUso.Usuario;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Usuario;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IGetAllUsuarios, GetAllUsuarios>();

//**********************************************************//

builder.Services.AddScoped<IFindAllCabania, FindAllCabania>();
builder.Services.AddScoped<IAltaCabania, AltaCabania>();
builder.Services.AddScoped<IFindByIdCabania, FindByIdCabania>();
builder.Services.AddScoped<IFindByFiltroCabania, GetCabaniaPorFiltros>();

//************************************************************//

builder.Services.AddScoped<IAltaMtto, AltaMtto>();
builder.Services.AddScoped<IFindByFechas, FindByFechas>();
builder.Services.AddScoped<IGetAllMttoXCab, GetAllMttoXCab>();
builder.Services.AddScoped<IGetMttoGroupBy, GetMttoGroupBy>();
builder.Services.AddScoped<IFindCabaniasFiltrado, FindCabaniasFiltrado>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var ruta = System.IO.Path.Combine(AppContext.BaseDirectory, "Libreria.WebApi.xml");
builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments(ruta));


var claveDificil = "F6j3#hC$xIKEdjCnx^5lvUb8";
var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //Definir las verificaciones a realizar
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //Definir los valores a intercambiar
            ValidIssuer = "identificadorEmisor",
            ValidAudience = "identificadorAudiencia",
            IssuerSigningKey = claveDificilEncriptada
        };

    });

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
