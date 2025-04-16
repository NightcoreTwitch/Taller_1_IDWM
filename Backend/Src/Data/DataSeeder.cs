using System;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Models;

namespace Backend.Src.Data;

public class DataSeeder
{
    public static void InitDb(WebApplication app){
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>()
            ?? throw new InvalidOperationException("DataContext not found");
        
        SeedData(context);
    }
    public static void SeedData(DataContext context)
    {
        context.Database.Migrate();

        if (context.Users.Any())
            return;
        
        var admin1 = new User(){
            Names = "Ignacio",
            LastNames = "Mancilla",
            Email = "ignacio.mancilla@gmail.com",
            PhoneNumber = "957519245",
            BirthDate = new DateOnly(2002, 08, 31),
            Password = "Pa$$word2025"
        };

        var faker = new Faker<User>()
            .RuleFor(u => u.Names, f => f.Name.FirstName())
            .RuleFor(u => u.LastNames, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.BirthDate, f => f.Date.Past(18))
            .RuleFor(u => u.Password, f => f.Internet.Password());

        var users = faker.Generate(9);

        context.Set<User>().Add(admin1);
        context.Set<User>().AddRange(users);
        context.SaveChanges();
    }
}
