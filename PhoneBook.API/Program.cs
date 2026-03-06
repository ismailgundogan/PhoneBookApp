using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Business;
using PhoneBook.Business.Validators;
using PhoneBook.DataAccess.Context;
using PhoneBook.DataAccess.Interfaces;
using PhoneBook.DataAccess.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5433;Database=PhoneBookDb;Username=postgres;Password=saw"));

builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));// Auto Mapper ve Microsoft Automapper Dependecy Injection Extensşion pakjetleri yüklü olması lazım ve aynı sürümde olması lazım

builder.Services.AddValidatorsFromAssemblyContaining<PersonelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
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
