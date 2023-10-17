using _Rest_Api_.CharacterController;
using _Rest_Api_.Services.CharacterService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System;
using _Rest_Api_.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapGet("/ping", async (ICharacterService characterService) =>
{
    var controller = new CharacterController(characterService);
    var result = controller.Ping();
    return result;
});
app.MapGet("/dogs", async (ICharacterService characterService, [FromQuery] string attribute, [FromQuery] string order, [FromQuery] int? page, [FromQuery] int? size) =>
{
    var controller = new CharacterController(characterService);
    var result = controller.GetDogs(attribute, order, page, size);
    return result;
});
app.MapPost("/dog", async (ICharacterService characterService, [FromBody] Character newcharacter) =>
{
    var controller = new CharacterController(characterService);
    var result = await controller.AddDogs(newcharacter);
    return result;
});


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})


.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
