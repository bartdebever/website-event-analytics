using BartdeBever.EventTracking.Attributes;
using BartdeBever.EventTracking.Dtos;
using BartdeBever.EventTracking.Models;
using BartdeBever.EventTracking.Services;
using Microsoft.AspNetCore.Mvc;

namespace BartdeBever.EventTracking.Controllers;

/// <summary>
/// API controller for tracking and ingesting events
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    /// Creates a new event log entry
    /// </summary>
    /// <param name="dto">The event data to log</param>
    /// <returns>The created event log entry</returns>
    [HttpPost]
    [ApiKeyAuthentication]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EventLog>> CreateEventLog([FromBody] CreateEventLogDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var application = HttpContext.Items["Application"] as Application;
        if (application == null)
        {
            return Unauthorized(new { error = "Application not found" });
        }

        var eventLog = await _eventService.CreateEventLogAsync(dto, application.Id);
        return CreatedAtAction(nameof(GetEventLog), new { id = eventLog.Id }, eventLog);
    }

    /// <summary>
    /// Retrieves an event log entry by ID
    /// </summary>
    /// <param name="id">The event log ID</param>
    /// <returns>The event log entry</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventLog>> GetEventLog(long id)
    {
        var eventLog = await _eventService.GetEventLogByIdAsync(id);

        if (eventLog == null)
        {
            return NotFound();
        }

        return Ok(eventLog);
    }
}
