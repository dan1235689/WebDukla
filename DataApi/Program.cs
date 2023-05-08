using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON file path
builder.Services.AddSingleton<JsonDataAccess<IRider>>(new JsonDataAccess<IRider>($"./data/riders.json"));
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Use CORS middleware
app.UseCors("AllowAllOrigins");

#region Riders
    app.MapGet("/riders", async (HttpContext context, JsonDataAccess<IRider> dataAccess) =>
{
    // Get all riders
    List<IRider> riders = await dataAccess.GetAllAsync();
    return Results.Ok(riders);
});

app.MapGet("/riders/{id:int}", async (int id, HttpContext context, JsonDataAccess<IRider> dataAccess) =>
{
    // Get rider by id
    IRider? rider = await dataAccess.GetByIdAsync(id);
    if (rider == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(rider);
});

app.MapPost("/riders", async (HttpContext context, JsonDataAccess<IRider> dataAccess) =>
{
    // Create a new rider from request body
    IRider? rider = await context.Request.ReadFromJsonAsync<IRider>();
    if (rider == null)
    {
        return Results.BadRequest();
    }
    await dataAccess.AddAsync(rider);
    return Results.Created($"/riders/{rider.Id}", rider);
});

app.MapPut("/riders/{id:int}", async (int id, HttpContext context, JsonDataAccess<IRider> dataAccess) =>
{
    // Update rider by id with new data from request body
    IRider? rider = await context.Request.ReadFromJsonAsync<IRider>();
    if (rider == null || id != rider.Id)
    {
        return Results.BadRequest();
    }
    await dataAccess.UpdateAsync(rider);
    return Results.NoContent();
});

app.MapDelete("/riders/{id:int}", async (int id, JsonDataAccess<IRider> dataAccess) =>
{
    // Delete rider by id
    await dataAccess.DeleteAsync(id);
    return Results.NoContent();
});

#endregion

app.Run();
