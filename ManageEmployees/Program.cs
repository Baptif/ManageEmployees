using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ManageEmployees");
builder.Services.AddDbContext<ManageEmployeeDbContext>(options => options.UseSqlServer(connectionString));

// Ajout des repositories
builder.Services.AddScoped<IDepartementRepository, DepartementRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// Ajout des services
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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
