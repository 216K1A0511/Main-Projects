using IDEAL.ERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using IDEAL.ERP.Application.Common.Interfaces;

namespace IDEAL.ERP.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<TodoTask> TodoTasks => Set<TodoTask>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<PlacementDrive> PlacementDrives => Set<PlacementDrive>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Notice> Notices => Set<Notice>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<News> LatestNews => Set<News>();

    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<CollegeSetup> CollegeSetups => Set<CollegeSetup>();
    public DbSet<AcademicSetup> AcademicSetups => Set<AcademicSetup>();
    public DbSet<ExtracurricularSetup> ExtracurricularSetups => Set<ExtracurricularSetup>();
    public DbSet<ComplaintParameter> ComplaintParameters => Set<ComplaintParameter>();
    public DbSet<AcademicCalendarEntry> AcademicCalendarEntries => Set<AcademicCalendarEntry>();
    public DbSet<ProgramDetail> ProgramDetails => Set<ProgramDetail>();
    public DbSet<FeedbackParameter> FeedbackParameters => Set<FeedbackParameter>();
    public DbSet<GrievanceFeedback> GrievanceFeedbacks => Set<GrievanceFeedback>();
    public DbSet<EmployeeWorkingDay> EmployeeWorkingDays => Set<EmployeeWorkingDay>();
    public DbSet<EmployeeAttendance> EmployeeAttendances => Set<EmployeeAttendance>();
    public DbSet<AppModule> AppModules => Set<AppModule>();
    public DbSet<AcademicBranch> AcademicBranches => Set<AcademicBranch>();
    public DbSet<AcademicBatch> AcademicBatches => Set<AcademicBatch>();
    public DbSet<AcademicGroup> AcademicGroups => Set<AcademicGroup>();
    public DbSet<StudentGroupAssignment> StudentGroupAssignments => Set<StudentGroupAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Institution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasMany(e => e.Students).WithOne(e => e.Institution).HasForeignKey(e => e.InstitutionId);
            entity.HasMany(e => e.Faculties).WithOne(e => e.Institution).HasForeignKey(e => e.InstitutionId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RollNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.RollNumber).IsUnique();
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EmployeeId).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.EmployeeId).IsUnique();
        });

        modelBuilder.Entity<TodoTask>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        });
    }
}
