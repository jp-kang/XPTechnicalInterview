using Microsoft.EntityFrameworkCore;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Repositories;
using XPTechnicalInterview.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<FinancialProductRepository>();
builder.Services.AddScoped<InvestmentRepository>();

builder.Services.AddScoped<InvestmentService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<FinancialProductService>();

builder.Services.AddSingleton<PortfolioContext>();

var app = builder.Build();

using (var context = new PortfolioContext())
{
  context.Database.EnsureCreated();
}

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
