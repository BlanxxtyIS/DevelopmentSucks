using DevelopmentSucks.API.Extensions;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Application.Services.Identity.Register;
using DevelopmentSucks.Domain.Repositories;
using DevelopmentSucks.Domain.Repositories.Identity;
using DevelopmentSucks.Infrastructure.Identity.Auth;
using DevelopmentSucks.Infrastructure.Identity.Register;
using DevelopmentSucks.Infrastructure.Persistence;
using DevelopmentSucks.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/DevelopmentSucks-app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Создание приложения");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connString)
    );

    builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection("JwtSettings"));
    builder.Services.AddScoped<IJwtRepository, JwtRepository>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.ConfigureJWT(builder.Configuration);

    builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
    builder.Services.AddScoped<ICoursesService, CoursesService>();
    builder.Services.AddScoped<IChaptersRepository, ChaptersRepository>();
    builder.Services.AddScoped<IChaptersService, ChaptersService>();
    builder.Services.AddScoped<ILessonsRepository, LessonsRepository>();
    builder.Services.AddScoped<ILessonsService, LessonsService>();

    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
    builder.Services.AddScoped<IAuthRepository, AuthRepository>();

    builder.Services.AddControllers();

    builder.Services.AddOpenApi();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Ошибка при старте приложения");
}
finally
{
    Log.CloseAndFlush();
}
