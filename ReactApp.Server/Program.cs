using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Models;
using ReactApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Regist IPersoninfoService
builder.Services.AddScoped<IPersoninfoService, PersoninfoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add Examcontext
builder.Services.AddDbContext<ExamContext>(
    option => option.UseSqlServer(builder.Configuration["DbConnectionString:Exam"]),
            ServiceLifetime.Singleton
);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
