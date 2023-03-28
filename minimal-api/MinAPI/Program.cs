using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MinApi.Data;
using MinApi.Dtos;
using MinApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnectionBuilder = new SqlConnectionStringBuilder();

sqlConnectionBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
// creation of user secrets in "user-secrets-creation.PNG" in the same commit
sqlConnectionBuilder.UserID = builder.Configuration["UserId"];
sqlConnectionBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionBuilder.ConnectionString));

builder.Services.AddScoped<ICommandRepo, SqlCommandRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/v1/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAllCommands();

    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (int id, ICommandRepo repo, IMapper mapper) =>
{
    var command = await repo.GetSingleCommandById(id);

    if (command == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mapper.Map<CommandReadDto>(command));
});

app.MapPost("api/v1/commands", async (CommandCreateDto commandCreateDto, ICommandRepo repo, IMapper mapper) =>
{
    var command = mapper.Map<Command>(commandCreateDto);

    await repo.CreateCommand(command);

    await repo.SaveChanges();

    var commandReadDto = mapper.Map<CommandReadDto>(command);

    return Results.Created($"api/v1/commands/{command.Id}", commandReadDto);
});

app.MapPut("api/v1/commands/{id}", async (int id, CommandUpdateDto commandUpdateDto, ICommandRepo repo, IMapper mapper) =>
{
    var command = await repo.GetSingleCommandById(id);

    if (command == null)
    {
        return Results.NotFound();
    }

    mapper.Map(commandUpdateDto, command);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("api/v1/commands/{id}", async (int id, ICommandRepo repo, IMapper mapper) =>
{
    var command = await repo.GetSingleCommandById(id);

    if (command == null)
    {
        return Results.NotFound();
    }

    repo.DeleteCommand(command);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.Run();

