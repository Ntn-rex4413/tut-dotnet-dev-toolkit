using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Dtos;
using PersonAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLDbConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/v1/people", async (AppDbContext context, IMapper mapper) =>
{
    var people = await context.People.ToListAsync();

    return Results.Ok(mapper.Map<IEnumerable<PersonReadDto>>(people));
});

app.MapGet("api/v1/people/{id}", async (AppDbContext context, int id, IMapper mapper) =>
{
    var person = await context.People.FindAsync(id);

    if (person == null)
    {
        return Results.NotFound();
    }

    var personDto = mapper.Map<PersonReadDto>(person);

    return Results.Ok(personDto);
});

app.MapPost("api/v1/people", async (AppDbContext context, PersonCreateDto personCreateDto, IMapper mapper) =>
{
    var person = mapper.Map<Person>(personCreateDto);

    await context.People.AddAsync(person);

    await context.SaveChangesAsync();

    return Results.Created($"/api/v1/people/{person.Id}", mapper.Map<PersonReadDto>(person));
});

app.MapPut("api/v1/people/{id}", async (AppDbContext context, int id, PersonUpdateDto personUpdateDto, IMapper mapper) =>
{
    var personModel = await context.People.FindAsync(id);

    if (personModel == null)
    {
        return Results.NotFound();
    }

    mapper.Map(personUpdateDto, personModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/v1/people/{id}", async (AppDbContext context, int id) =>
{
    var person = await context.People.FindAsync(id);

    if (person == null)
    {
        return Results.NotFound();
    }

    context.People.Remove(person);

    await context.SaveChangesAsync();

    return Results.Ok(person);
});

app.Run();

