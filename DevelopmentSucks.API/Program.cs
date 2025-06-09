using DevelopmentSucks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString)
);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // Пример: выводим все курсы в консоль
//    var courses = await dbContext.Courses.ToListAsync();

//    foreach (var course in courses)
//    {
//        Console.WriteLine($"Курс: {course.Title} - {course.Description}");
//    }
//}

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

    var courses = await db.Courses
        .Include(c => c.Chapters)
        .ToListAsync();

    Console.WriteLine("====== Courses in DB ========");
    foreach (var course in courses)
    {
        Console.WriteLine($"ID: {course.Id}, DESCRIPTOPN: {course.Description}, TITLE: {course.Title}");
        foreach(var chapter in course.Chapters)
        {
            Console.WriteLine($"CHAPTER: {chapter.Title}, ID: {chapter.Id}");
        }
        Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
    }
}

app.Run();
