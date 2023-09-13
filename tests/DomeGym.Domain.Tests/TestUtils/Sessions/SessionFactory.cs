using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession (
        string name = Constants.Session.Name,
        string description = Constants.Session.Description,
        Guid? roomId = null,
        Guid? trainerId = null,
        short maxParticipants = Constants.Session.MaxParticipants,
        DateOnly? date = null,
        TimeRange? time = null,
        List<SessionCategory>? categories = null,
        Guid? id = null)
    {
        return new Session(
            name, description,
            maxParticipants: maxParticipants, 
            roomId: roomId ?? Constants.Rooms.Id,
            trainerId: trainerId ?? Constants.Trainer.Id,
            date: date ?? Constants.Session.Date, 
            time: time ?? Constants.Session.Time, 
            categories: categories ?? Constants.Session.Categories, 
            id: id ?? Constants.Session.Id);
    }
}