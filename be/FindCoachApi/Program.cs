using FindCoachApi.Data;
using FindCoachApi.Services;
using FindCoachApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//DI services
builder.Services.AddScoped<IAuthService, Auth>();
builder.Services.AddScoped<ICoachesService, Coaches>();
builder.Services.AddScoped<IRequestService, Request>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:8080"));

app.UseAuthorization();

app.MapControllers();

app.Run();
