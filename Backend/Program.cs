// imports
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Data;
using Backend.Src.Models;

using Backend.Src.Interfaces;
using Backend.Src.Repositories;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(
        builder => {
            builder.WithOrigins("http://localhost:3000");
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

DataSeeder.InitDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseHttpsRedirection();
app.MapControllers();
app.Run();