using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom (
        string name = Constants.Rooms.Name,
        int maxDailySession = Constants.Rooms.MaxDailySessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            name: name, 
            gymId: gymId ?? Constants.Gyms.Id, 
            maxDailySession: maxDailySession, 
            id: id ?? Constants.Rooms.Id);
    }
}