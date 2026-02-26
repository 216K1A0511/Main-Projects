using IDEAL.ERP.Infrastructure.Persistence;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IDEAL.ERP.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Institutions.AnyAsync()) return;

        var institution = new Institution { Name = "IDEAL College of Engineering", Code = "ICE001" };
        context.Institutions.Add(institution);

        context.Students.AddRange(
            new Student { FirstName = "John", LastName = "Doe", RollNumber = "S001", Department = "Computer Science", Institution = institution },
            new Student { FirstName = "Jane", LastName = "Smith", RollNumber = "S002", Department = "Electronics", Institution = institution }
        );

        context.Faculties.AddRange(
            new Faculty { FirstName = "Dr. Robert", LastName = "Brown", EmployeeId = "F001", Department = "Computer Science", Institution = institution },
            new Faculty { FirstName = "Sarah", LastName = "Wilson", EmployeeId = "F002", Department = "Mechanical", Institution = institution }
        );

        context.TodoTasks.AddRange(
            new TodoTask { Title = "Review Exam Papers", Priority = TaskPriority.High, Status = IDEAL.ERP.Domain.Enums.TaskStatus.Pending, ReminderDateTime = DateTime.UtcNow.AddHours(2) },
            new TodoTask { Title = "Update Website News", Priority = TaskPriority.Medium, Status = IDEAL.ERP.Domain.Enums.TaskStatus.InProgress }
        );

        context.LatestNews.Add(new News { Title = "Annual Sports Meet 2026", Content = "The annual sports meet will be held next month." });
        context.Notices.Add(new Notice { Title = "Holiday Announcement", Content = "College will remain closed on Friday.", IsPublished = true });
        
        if (!context.Holidays.Any())
        {
            context.Holidays.Add(new Holiday { Name = "Christmas", StartDate = new DateTime(DateTime.UtcNow.Year, 12, 25), EndDate = new DateTime(DateTime.UtcNow.Year, 12, 25) });
            // context.SaveChanges() is not needed here as SaveChangesAsync() is called at the end of the method.
        }
        context.Events.Add(new Event { Title = "Tech Fest 2026", EventDate = DateTime.UtcNow.AddDays(10), Location = "Auditorium" });

        await context.SaveChangesAsync();
    }
}
