using DiApi.Data;
using DiApi.DataServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDataRepo, SqlDataRepo>();

builder.Services.AddScoped<IDataService, HttpDataService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/getdata", (IDataRepo repo) =>
{

    repo.ReturnData();

    return Results.Ok();
});


app.Run();

