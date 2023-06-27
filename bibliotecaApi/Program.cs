using bibliotecaApi.Models;
using bibliotecaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BibliotecaDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaBDContext"));
});

builder.Services.AddScoped<LibroService>();
builder.Services.AddScoped<LectorService>();
builder.Services.AddScoped<PrestamoService>();

builder.Services.AddControllers();
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
