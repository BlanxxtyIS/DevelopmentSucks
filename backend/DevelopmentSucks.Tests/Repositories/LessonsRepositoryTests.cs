using DevelopmentSucks.Domain.Common.FilterParameters;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Infrastructure.Persistence;
using DevelopmentSucks.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentSucks.Tests.Repositories;

public class LessonsRepositoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // отдельная БД для каждого теста
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetLessons_Should_Filter_And_Paginate()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repo = new LessonsRepository(context);

        var lessons = new List<Lesson>
        {
            new Lesson {Id = Guid.NewGuid(), Title = "Lesson 1", Order = 0 },
            new Lesson {Id = Guid.NewGuid(), Title = "Lesson 2", Order = 1 },
            new Lesson {Id = Guid.NewGuid(), Title = "Lesson 3", Order = 2 },
            new Lesson {Id = Guid.NewGuid(), Title = "Lesson 4", Order = 3 }
        };

        await context.Lessons.AddRangeAsync(lessons);
        await context.SaveChangesAsync();

        var parameters = new LessonFilterParameters
        {
            MinOrder = 2,
            MaxOrder = 3,
            PageNumber = 1,
            PageSize = 10
        };

        //Act
        var result = await repo.GetLessons(parameters);

        //Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, l => Assert.InRange(l.Order, 2, 3));
    }

    [Fact]
    public async Task CreateLesson_Should_Save_And_ReturnId()
    {
        var context = GetInMemoryDbContext();
        var repo = new LessonsRepository(context);

        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = "Test Lesson",
            Content = "Some content",
            Order = 1
        };

        //Act
        var id = await repo.CreateLesson(lesson);
        var retrived = await repo.GetLesson(id);

        //Assert
        Assert.Equal(lesson.Title, retrived?.Title);
        Assert.Equal(lesson.Content, retrived?.Content);
    }

    [Fact]
    public async Task UpdateLesson_Should_Modify_Existing()
    {
        var context = GetInMemoryDbContext();
        var repo = new LessonsRepository(context);

        var lesson = new Lesson { 
            Id = Guid.NewGuid(), 
            Title = "Original",
            Content = "Text", Order = 1 
        };

        lesson.Title = "Updated";
        var result = await repo.UpdateLesson(lesson);

        var updated = await repo.GetLesson(lesson.Id);

        Assert.True(result);
        Assert.Equal("Updated", updated?.Title);
    }

    [Fact]
    public async Task DeleteLesson_Should_Remove_Entity()
    {
        var context = GetInMemoryDbContext();
        var repo = new LessonsRepository(context);

        var lesson = new Lesson { Id = Guid.NewGuid(), Title = "To delete", Order = 1 };
        await repo.CreateLesson(lesson);

        var deleted = await repo.DeleteLesson(lesson.Id);
        var retrieved = await repo.GetLesson(lesson.Id);

        Assert.True(deleted);
        Assert.Null(retrieved);
    }
}
