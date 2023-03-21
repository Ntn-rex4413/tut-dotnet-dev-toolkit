using DiApi.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/getdata", () =>
{
    //var repo = new SqlDataRepo();
    var repo = new NoSqlDataRepo();

    repo.ReturnData();

    return Results.Ok();
});


app.Run();

