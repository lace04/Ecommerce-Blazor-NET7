using Ecommerce.Repositorio.DBContext;
using Microsoft.EntityFrameworkCore;

using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.Implementacion;

using Ecommerce.utilidades;

using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbecommerceContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});


builder.Services.AddTransient(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddTransient<IProductoServicio, ProductoServicio>();
builder.Services.AddTransient<IVentaServicio, VentaServicio>();
builder.Services.AddTransient<IDashboardServicio, DashboardServicio>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy",
       app => app.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
