using AutoMapper;
using ClientManagerBLL;
using ClientManagerBLL.Dtos;
using ClientManagerBLL.Servicios;
using ClientManagerDAL.Entities;
using ClientManagerDAL.Repositorios.Clientes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// **DI: Repositorio y Servicio**
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();

// **DI: AutoMapper**
builder.Services.AddAutoMapper(typeof(MapeoClases)); 

var app = builder.Build();

// Configure the HTTP request pipeline
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