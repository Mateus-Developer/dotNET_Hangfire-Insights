using AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;
using AcmeCompany.SmartHR.Application.QueryHandlers.EmployeeQuerys;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Extensions;
using Scheduler.Api.Models;

namespace Scheduler.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IEmployeeQueryHandler _employeeQueryHandler;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IMediator mediator, IEmployeeQueryHandler employeeQueryHandler, ILogger<EmployeeController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _employeeQueryHandler = employeeQueryHandler ?? throw new ArgumentNullException(nameof(employeeQueryHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post(CancellationToken cancellationToken)
    {
        var command = new Faker<HireEmployeeCommand>().CustomInstantiator(f
            => new HireEmployeeCommand(
                f.Person.FullName,
                f.Person.Email,
                f.Date.Between(DateTime.Now.AddDays(7), DateTime.Now.AddMonths(1)),
                f.Name.JobTitle(),
                f.Random.Int(1500, 20000)))
            .Generate(1)
            .FirstOrDefault()!;

        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpPost("UrgentNotification")]
    public IActionResult PostUrgentNotification([FromBody] UrgentNotificationCommand command, CancellationToken cancellationToken)
    {
        var jobName = _mediator.Enqueue("UrgentNotification", command, cancellationToken);

        _logger.LogInformation("Job {JobName} created successfully.", jobName);

        return NoContent();
    }

    [HttpPost("ScheduleNotification")]
    public IActionResult PostScheduleNotification([FromBody] ScheduleEmailCommand command, CancellationToken cancellationToken)
    {
        var jobName = _mediator.Scheduler("ScheduleNotification", command, command.SchedulerAt, cancellationToken);

        _logger.LogInformation("Job {JobName} created successfully.", jobName);

        return NoContent();
    }

    [HttpPost("RecurringNotification")]
    public IActionResult PostScheduleNotification([FromBody] RecurringNotificationCommand command, CancellationToken cancellationToken)
    {
        _mediator.Recurring("ScheduleNotification", command, command.Cron, cancellationToken);

        _logger.LogInformation("Job {JobName} created successfully.", "RecurringNotification");

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _employeeQueryHandler.GetByIdAsync(id, cancellationToken));
    }

    [HttpGet("PaginateList/{pageNumber:int}/{rowsOfPage:int}")]
    public async ValueTask<IActionResult> GetPaginatedList(int pageNumber = 1, int rowsOfPage = 5, CancellationToken cancellationToken = default)
    {
        return Ok(await _employeeQueryHandler.ListWithPaginationAsync(pageNumber, rowsOfPage, cancellationToken));
    }
}
