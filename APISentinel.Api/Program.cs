using Dapper;
using Microsoft.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<SqlConnection>(_ =>
//add scoped means : So, 1 instance will work for each HTTP request.
{
    var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    //this particular lines looks for the configuration key 
    // .i..e ConnectionString: DefaultConnection
    ?? throw new InvalidOperationException(
    "Database connection default string not found");

    return new SqlConnection(connectionString);
    //this creates a c# object that can connect with our SQL database.
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/db-test", async (SqlConnection connection) =>
{
    await connection.OpenAsync();

    var database = await connection.QuerySingleAsync<string>(
        "SELECT DB_NAME()");

    return Results.Ok(new { Database = database });
});

app.Run();



