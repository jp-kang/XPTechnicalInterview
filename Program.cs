using Microsoft.EntityFrameworkCore;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;
using XPTechnicalInterview.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<FinancialProductRepository>();
builder.Services.AddScoped<InvestmentRepository>();
builder.Services.AddScoped<InvestmentService>();

builder.Services.AddSingleton<PortfolioContext>();


//using (var context = new PortfolioContext())
//{
//    context.Database.Migrate();
//}


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
