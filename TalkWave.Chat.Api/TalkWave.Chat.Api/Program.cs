using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TalkWave.Chat.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

string? connectionStringUserDB = builder.Configuration.GetConnectionString("ChatDB");

builder.Services
    .AddApplicationDbContext(connectionStringUserDB)
    .AddApplicationAutoMapper()
    .AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
