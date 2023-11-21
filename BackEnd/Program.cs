using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

var builder = WebApplication.CreateBuilder(args);

string connect = "Server=localhost;Database=mydatabase;User=idealsoft;Password=idealsoft123;";

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString(connect)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
