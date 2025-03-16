using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TalkWave.User.Api.Core.Interfaces;
using TalkWave.User.Api.Core.Services;
using TalkWave.User.Api.Core.Validations;
using TalkWave.User.Data.DataContexts;
using TalkWave.User.Data.Interfaces;
using TalkWave.User.Data.MappingProfilies;
using TalkWave.User.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add components to DI

//Add connection to data base
string? connectionStringUserDB = builder.Configuration.GetConnectionString("UserDB");
builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(connectionStringUserDB));


//Add validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

//Add mapper profilies
builder.Services.AddAutoMapper(typeof(UserProfile));

//Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Add repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => {
    options.AddPolicy("UIPolicy",
        builder => {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UIPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
