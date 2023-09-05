using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom (
        int maxDailySession = Constants.Rooms.MaxDailySessions, 
        Guid? id = null)
    {
        return new Room(
            maxDailySession, 
            id: id ?? Constants.Rooms.Id);
    }
}