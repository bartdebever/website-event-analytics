using System.Text.Json;
using BartdeBever.EventTracking.Dtos;
using BartdeBever.EventTracking.Mappers;
using BartdeBever.EventTracking.Models;
using BartdeBever.EventTracking.Repositories;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace BartdeBever.EventTracking.Services;

public interface IEventService
{
    Task<EventLog> CreateEventLogAsync(CreateEventLogDto dto, Guid applicationId);
    Task<EventLog?> GetEventLogByIdAsync(long id);
}

public class EventService : IEventService
{
    private readonly IEventLogRepository _repository;
    private readonly TelemetryClient? _telemetryClient;
    private readonly ILogger<EventService> _logger;

    public EventService(
        IEventLogRepository repository,
        ILogger<EventService> logger,
        TelemetryClient? telemetryClient = null)
    {
        _repository = repository;
        _logger = logger;
        _telemetryClient = telemetryClient;
    }

    public async Task<EventLog> CreateEventLogAsync(CreateEventLogDto dto, Guid applicationId)
    {
        var eventLog = EventLogMapper.CreateEventLogDto(dto);
        eventLog.ApplicationId = applicationId;

        try
        {
            var createdEvent = await _repository.CreateAsync(eventLog);

            TrackEventToApplicationInsights(createdEvent);

            _logger.LogInformation(
                "Event log created: {EventName} with ID {EventLogId} for session {SessionId} and application {ApplicationId}",
                createdEvent.EventName,
                createdEvent.Id,
                createdEvent.SessionId,
                createdEvent.ApplicationId);

            return createdEvent;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error creating event log for event {EventName}", dto.EventName);
            throw;
        }
    }

    public Task<EventLog?> GetEventLogByIdAsync(long id)
    {
        return _repository.GetByIdAsync(id);
    }

    private void TrackEventToApplicationInsights(EventLog eventLog)
    {
        if (_telemetryClient == null)
        {
            return;
        }

        var eventTelemetry = new EventTelemetry(eventLog.EventName)
        {
            Timestamp = eventLog.CreatedAt,
            Properties =
            {
                ["SessionId"] = eventLog.SessionId.ToString(),
                ["EventLogId"] = eventLog.Id.ToString(),
                ["ApplicationId"] = eventLog.ApplicationId.ToString()
            }
        };

        if (!string.IsNullOrEmpty(eventLog.Url))
        {
            eventTelemetry.Properties["Url"] = eventLog.Url;
        }

        if (eventLog.Data != null)
        {
            eventTelemetry.Properties["Data"] = JsonSerializer.Serialize(eventLog.Data);
        }

        _telemetryClient.TrackEvent(eventTelemetry);
    }
}
