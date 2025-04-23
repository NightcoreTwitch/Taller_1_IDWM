using System;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Backend.Src.DTOs;
using Backend.Src.Mappers;

namespace Backend.Src.Data;

public class DataSeeder
{
    public static void InitDb(WebApplication app){
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>()
            ?? throw new InvalidOperationException("UserManager not found");
        var context = scope.ServiceProvider.GetRequiredService<DataContext>()
            ?? throw new InvalidOperationException("DataContext not found");
        
        SeedData(context, userManager);
    }
    public static void AdressDb(WebApplication app){
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>()
            ?? throw new InvalidOperationException("UserManager not found");
        var context = scope.ServiceProvider.GetRequiredService<DataContext>()
            ?? throw new InvalidOperationException("DataContext not found");
        
        SeedAdresses(context, userManager);
    }
    public static void SeedData(DataContext context, UserManager<User> userManager)
    {
        context.Database.Migrate();

        if (!context.Users.Any()){
            var admin1 = new RegisterDTO(){
            Names = "Ignacio",
            LastNames = "Mancilla",
            Email = "ignacio.mancilla@gmail.com",
            PhoneNumber = "957519245",
            BirthDate = new DateOnly(2002, 08, 31),
            Password = "Pa$$word2025",
            ConfirmPassword = "Pa$$word2025"
            };

            var faker = new Faker<RegisterDTO>()
                .RuleFor(u => u.Names, f => f.Name.FirstName())
                .RuleFor(u => u.LastNames, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(18, DateTime.Now)))
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.ConfirmPassword, (f, u) => u.Password);

            var users = faker.Generate(4);

            var newAdmin = UserMapper.MapToUser(admin1);
            newAdmin.UserName = newAdmin.Email;
            var result = userManager.CreateAsync(newAdmin, admin1.Password);
            if (!result.Result.Succeeded)
            {
                throw new Exception("Failed to create admin user.");
            }
            var role = userManager.AddToRoleAsync(newAdmin, "Admin");
            if (!role.Result.Succeeded)
            {
                throw new Exception("Failed to assign admin role.");
            }
            foreach (var user in users)
            {
                var newUser = UserMapper.MapToUser(user);
                newUser.UserName = newUser.Email;
                var resultUser = userManager.CreateAsync(newUser, user.Password);
                if (!resultUser.Result.Succeeded)
                {
                    throw new Exception("Failed to create user.");
                }
                var roleUser = userManager.AddToRoleAsync(newUser, "User");
                if (!roleUser.Result.Succeeded)
                {
                    throw new Exception("Failed to assign user role.");
                }
            }

            context.SaveChanges();
        }
    }
    public static void SeedAdresses(DataContext context, UserManager<User> userManager)
    {
        if (!context.Adresses.Any()){
            var adminUser = userManager.FindByEmailAsync("ignacio.mancilla@gmail.com").Result;
            var adminAdress1 = new Adress(){
                Street = "Hernan Cortes",
                Number = "2637",
                City = "Calama",
                Region = "Antofagasta",
                ZipCode = "1390000",
                UserId = adminUser.Id
            };

            context.Set<Adress>().AddRange(adminAdress1);
            context.SaveChanges();
        }
    }
}
