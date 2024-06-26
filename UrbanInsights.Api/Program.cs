using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Urban Insights API", Version = "v1" });
});
builder.Services.AddDbContext<UrbanInsightsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseRouting();

app.UseAuthorization();


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

app.Run();

