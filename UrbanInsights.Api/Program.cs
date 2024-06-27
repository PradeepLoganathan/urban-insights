using Microsoft.EntityFrameworkCore;
using UrbanInsights.Api.Data;
using UrbanInsights.Api.Models;



var builder = WebApplication.CreateBuilder(args);



// Load configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Urban Insights API", 
        Version = "v1" 
    });
});
builder.Services.AddDbContext<UrbanInsightsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GreenplumConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Sample endpoint
app.MapGet("/", () => "Welcome to Urban Insights API!");

// Add more endpoints here
app.MapGet("/locations", async (UrbanInsightsDbContext db) => await db.Locations.ToListAsync());

app.MapGet("/locations/{id}", async (int id, UrbanInsightsDbContext db) =>
    await db.Locations.FindAsync(id)
        is Location location
            ? Results.Ok(location)
            : Results.NotFound());

app.MapPost("/locations", async (Location location, UrbanInsightsDbContext db) =>
{
    db.Locations.Add(location);
    await db.SaveChangesAsync();
    return Results.Created($"/locations/{location.Id}", location);
});

app.MapPut("/locations/{id}", async (int id, Location inputLocation, UrbanInsightsDbContext db) =>
{
    var location = await db.Locations.FindAsync(id);

    if (location is null) return Results.NotFound();

    location.Name = inputLocation.Name;
    location.Latitude = inputLocation.Latitude;
    location.Longitude = inputLocation.Longitude;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/locations/{id}", async (int id, UrbanInsightsDbContext db) =>
{
    if (await db.Locations.FindAsync(id) is Location location)
    {
        db.Locations.Remove(location);
        await db.SaveChangesAsync();
        return Results.Ok(location);
    }

    return Results.NotFound();
});

app.MapGet("/locations/nearby", async (double latitude, double longitude, double radius, UrbanInsightsDbContext db) =>
{
    var nearbyLocations = await db.Locations
        .FromSqlRaw("SELECT * FROM locations WHERE ST_DWithin(ST_SetSRID(ST_Point({0}, {1}), 4326)::geography, geography::geometry, {2})", longitude, latitude, radius)
        .ToListAsync();

    return Results.Ok(nearbyLocations);
});

app.Run();

