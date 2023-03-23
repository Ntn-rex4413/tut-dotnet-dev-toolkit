using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLDbConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET single
app.MapGet("api/v1/commands/{commandId}", async (AppDbContext context, string commandId) =>
{
    var command = await context.Commands.FirstOrDefaultAsync(c => c.CommandId == commandId);

    if (command != null)
    {
        return Results.Ok(command);
    }
    else
    {
        return Results.NotFound();
    }
});

// GET multiple
app.MapGet("api/v1/commands", async (AppDbContext context) =>
{
    var commands = await context.Commands.ToListAsync();

    return Results.Ok(commands);
});

// Create
// good practice is usually working with a data transfer object instead of
// using the model like here
app.MapPost("api/v1/commands", async (AppDbContext context, Command cmd) =>
{
    await context.Commands.AddAsync(cmd);

    await context.SaveChangesAsync();

    return Results.Created($"/api/v1/commands/{cmd.CommandId}", cmd);
});

// Update
// string commandId in route + object in json body
app.MapPut("api/v1/commands/{commandId}", async (AppDbContext context, string commandId, Command cmd) =>
{
    var command = await context.Commands.FirstOrDefaultAsync(c => c.CommandId == commandId);

    if (command is null)
    {
        return Results.NotFound();
    }

    command.HowTo = cmd.HowTo;
    command.CommandLine = cmd.CommandLine;
    command.Platform = cmd.Platform;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

// Delete
app.MapDelete("api/v1/commands/{commandId}", async (AppDbContext context, string commandId) =>
{
    var command = await context.Commands.FirstOrDefaultAsync(c => c.CommandId == commandId);

    if (command is null)
    {
        return Results.NotFound();
    }

    // not an async operation
    context.Commands.Remove(command);

    await context.SaveChangesAsync();

    return Results.Ok(command);
});


app.Run();
