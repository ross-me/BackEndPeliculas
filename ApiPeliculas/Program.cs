using ApiCine.Contexts;
using Microsoft.EntityFrameworkCore;
using ApiCine.Repository;
using ApiCine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<CineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CineConnection")));

builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

builder.Services.AddScoped<ISalaCineRepository, SalaCineRepository>();
builder.Services.AddScoped<ISalaCineService, SalaCineService>();


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder.WithOrigins("http://localhost:4200") 
            .AllowAnyMethod() 
            .AllowAnyHeader()); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
