using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi; // Добавь это вручную, если подчеркнет

var builder = WebApplication.CreateBuilder(args);

// Настройка БД
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddControllers();

// Регистрация Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();