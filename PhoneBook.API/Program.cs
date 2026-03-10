using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Business;
using PhoneBook.Business.Middleware;
using PhoneBook.Business.Validators;
using PhoneBook.DataAccess.Context;
using PhoneBook.DataAccess.Interfaces;
using PhoneBook.DataAccess.Repo;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5433;Database=PhoneBookDb;Username=postgres;Password=saw"));

builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile)); // Auto Mapper ve Microsoft Automapper Dependecy Injection Extensşion pakjetleri yüklü olması lazım ve aynı sürümde olması lazım

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PersonelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog Yapılandırması
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Konsola yaz
    .WriteTo.File("logs/phonebook-log.txt", rollingInterval: RollingInterval.Day) // Her gün yeni dosya aç
    .CreateLogger();

builder.Host.UseSerilog();

// 1. CORS Politikasını Tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseDefaultFiles(); 

// 2. Middleware olarak etkinleştir (Routing'den sonra, Authorization'dan önce)
app.UseCors("AllowAll");

app.UseMiddleware<ExceptionMiddleware>();

// Swagger sadece Development'ta çalışsın
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Telefon Rehberi API V1");
    });

    app.MapOpenApi();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();