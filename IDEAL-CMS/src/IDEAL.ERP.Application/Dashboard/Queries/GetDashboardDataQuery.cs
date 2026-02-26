using MediatR;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Application.Common.Interfaces;
using IDEAL.ERP.Domain.Entities;

namespace IDEAL.ERP.Application.Dashboard.Queries;

public record DashboardDto(
    int ActiveStudentsCount,
    int ActiveFacultiesCount,
    int JobApplicationsCount,
    List<InstitutionSummaryDto> Institutions,
    List<PlacementDriveDto> UpcomingPlacementDrives,
    List<EventDto> UpcomingEvents,
    List<NoticeDto> RecentNotices,
    List<HolidayDto> MonthlyHolidays,
    List<NewsDto> LatestNews,
    List<DepartmentStudentCountDto> DepartmentWiseStudents
);

public record InstitutionSummaryDto(Guid Id, string Name);
public record PlacementDriveDto(Guid Id, string CompanyName, DateTime Date);
public record EventDto(Guid Id, string Title, DateTime Date);
public record NoticeDto(Guid Id, string Title, DateTime CreatedAt);
public record HolidayDto(Guid Id, string Name, DateTime StartDate, DateTime EndDate);
public record NewsDto(Guid Id, string Title, DateTime CreatedAt);
public record DepartmentStudentCountDto(string Department, int Count);

public record GetDashboardDataQuery(Guid? InstitutionId) : IRequest<DashboardDto>;

public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, DashboardDto>
{
    private readonly IAppDbContext _context;

    public GetDashboardDataQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Students.AsQueryable();
        if (request.InstitutionId.HasValue)
        {
            query = query.Where(s => s.InstitutionId == request.InstitutionId.Value);
        }

        var activeStudents = await query.CountAsync(s => s.IsActive, cancellationToken);
        var activeFaculties = await _context.Faculties.CountAsync(f => f.IsActive, cancellationToken);
        var jobApps = await _context.JobApplications.CountAsync(cancellationToken);

        var institutions = await _context.Institutions
            .Select(i => new InstitutionSummaryDto(i.Id, i.Name))
            .ToListAsync(cancellationToken);

        var placementDrives = await _context.PlacementDrives
            .OrderBy(p => p.DriveDate)
            .Take(5)
            .Select(p => new PlacementDriveDto(p.Id, p.CompanyName, p.DriveDate))
            .ToListAsync(cancellationToken);

        var events = await _context.Events
            .Where(e => e.EventDate >= DateTime.UtcNow)
            .OrderBy(e => e.EventDate)
            .Take(5)
            .Select(e => new EventDto(e.Id, e.Title, e.EventDate))
            .ToListAsync(cancellationToken);

        var notices = await _context.Notices
            .Where(n => n.IsPublished)
            .OrderByDescending(n => n.CreatedAt)
            .Take(5)
            .Select(n => new NoticeDto(n.Id, n.Title, n.CreatedAt))
            .ToListAsync(cancellationToken);

        var holidays = await _context.Holidays
            .Where(h => h.StartDate.Month == DateTime.UtcNow.Month && h.StartDate.Year == DateTime.UtcNow.Year)
            .Select(h => new HolidayDto(h.Id, h.Name, h.StartDate, h.EndDate))
            .ToListAsync(cancellationToken);

        var news = await _context.LatestNews
            .OrderByDescending(n => n.CreatedAt)
            .Take(5)
            .Select(n => new NewsDto(n.Id, n.Title, n.CreatedAt))
            .ToListAsync(cancellationToken);

        var deptWise = await query
            .GroupBy(s => s.Department)
            .Select(g => new DepartmentStudentCountDto(g.Key, g.Count()))
            .ToListAsync(cancellationToken);

        return new DashboardDto(
            activeStudents,
            activeFaculties,
            jobApps,
            institutions,
            placementDrives,
            events,
            notices,
            holidays,
            news,
            deptWise
        );
    }
}
