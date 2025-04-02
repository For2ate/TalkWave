using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using TalkWave.Chat.Api.Configurations;
using TalkWave.Chat.Api.Extensions;
using TalkWave.Chat.Api.Hubs;
using TalkWave.Chat.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

string? connectionStringUserDB = builder.Configuration.GetConnectionString("ChatDB");
builder.Services.AddDbContext<ChatsContext>(options => options.UseNpgsql(connectionStringUserDB));

builder.Services.AddScoped<DbContext, ChatsContext>(provider =>
    provider.GetRequiredService<ChatsContext>());

builder.Services.AddSignalRCore();

builder.Services
    .AddApplicationAutoMapper()
    .AddApplicationServices()
    .AddApplicationRedis();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("/Chat");

app.MapControllers();

app.Run();
