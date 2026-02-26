using IDEAL.ERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDEAL.ERP.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Institution> Institutions { get; }
    DbSet<Student> Students { get; }
    DbSet<Faculty> Faculties { get; }
    DbSet<TodoTask> TodoTasks { get; }
    DbSet<JobApplication> JobApplications { get; }
    DbSet<PlacementDrive> PlacementDrives { get; }
    DbSet<Event> Events { get; }
    DbSet<Notice> Notices { get; }
    DbSet<Holiday> Holidays { get; }
    DbSet<News> LatestNews { get; }

    DbSet<Quote> Quotes { get; }
    DbSet<CollegeSetup> CollegeSetups { get; }
    DbSet<AcademicSetup> AcademicSetups { get; }
    DbSet<ExtracurricularSetup> ExtracurricularSetups { get; }
    DbSet<ComplaintParameter> ComplaintParameters { get; }
    DbSet<AcademicCalendarEntry> AcademicCalendarEntries { get; }
    DbSet<ProgramDetail> ProgramDetails { get; }
    DbSet<FeedbackParameter> FeedbackParameters { get; }
    DbSet<GrievanceFeedback> GrievanceFeedbacks { get; }
    DbSet<EmployeeWorkingDay> EmployeeWorkingDays { get; }
    DbSet<EmployeeAttendance> EmployeeAttendances { get; }
    DbSet<AppModule> AppModules { get; }
    DbSet<AcademicBranch> AcademicBranches { get; }
    DbSet<AcademicBatch> AcademicBatches { get; }
    DbSet<AcademicGroup> AcademicGroups { get; }
    DbSet<StudentGroupAssignment> StudentGroupAssignments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
