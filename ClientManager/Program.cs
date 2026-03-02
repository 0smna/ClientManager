using AutoMapper;
using ClientManagerBLL;
using ClientManagerBLL.Servicios;

using ClientManagerDAL.Repositorios.Clientes;
using ClientManagerDAL.Repositorios.Vehiculos;
using ClientManagerDAL.Repositorios.Citas;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// MVC
// =====================================
builder.Services.AddControllersWithViews();

// =====================================
// REPOSITORIOS
// =====================================
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorio>();
builder.Services.AddScoped<ICitaRepositorio, CitaRepositorio>();

// =====================================
// SERVICIOS
// =====================================
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<IVehiculoServicio, VehiculoServicio>();
builder.Services.AddScoped<ICitaServicio, CitaServicio>();

// =====================================
// AUTOMAPPER
// =====================================
builder.Services.AddAutoMapper(typeof(MapeoClases));

var app = builder.Build();

// =====================================
// PIPELINE
// =====================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();