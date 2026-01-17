using FC.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mainConfiguration = builder.Configuration;

builder.Services.AddDatabaseServices(
    "Main",
    mainConfiguration["DBConnections:Main:DbType"], 
    mainConfiguration["DBConnections:Main:ConntionString"]);

builder.Services.AddDatabaseServices(
    "History",
    mainConfiguration["DBConnections:History:DbType"],
    mainConfiguration["DBConnections:History:ConntionString"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
