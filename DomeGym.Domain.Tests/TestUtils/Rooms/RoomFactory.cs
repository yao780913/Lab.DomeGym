using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom (Guid? id = null)
    {
        return new Room(id ?? Constants.Rooms.Id);
    }
}