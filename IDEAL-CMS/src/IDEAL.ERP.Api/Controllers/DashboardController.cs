using MediatR;
using Microsoft.AspNetCore.Mvc;
using IDEAL.ERP.Application.Dashboard.Queries;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboardData([FromQuery] Guid? institutionId)
    {
        var result = await _mediator.Send(new GetDashboardDataQuery(institutionId));
        return Ok(result);
    }
}
