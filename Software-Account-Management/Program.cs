using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Software_Account_Management.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Software_Account_ManagementContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Software_Account_ManagementContext") ?? throw new InvalidOperationException("Connection string 'Software_Account_ManagementContext' not found.")));

// Add services to the container.

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

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
