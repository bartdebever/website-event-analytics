using BartdeBever.EventTracking.Dtos;
using BartdeBever.EventTracking.Models;

namespace BartdeBever.EventTracking.Mappers;

public static class EventLogMapper
{
    public static EventLog CreateEventLogDto(CreateEventLogDto createEventLogDto)
    {
        return new EventLog
        {
            EventName = createEventLogDto.EventName,
            SessionId = createEventLogDto.SessionId,
            Url = createEventLogDto.Url,
            Data = createEventLogDto.Data,
            CreatedAt = DateTime.UtcNow
        };
    }
}