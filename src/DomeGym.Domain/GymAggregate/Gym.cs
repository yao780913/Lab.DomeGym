﻿using DomeGym.Domain.Common;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = new ();

    public string Name { get; }

    public Guid SubscriptionId { get; }

    public Gym (string name, int maxRooms, Guid subscriptionId, Guid? id = null)
        :base (id ?? Guid.NewGuid())
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
    }
    
    

    public ErrorOr<Success> AddRoom (Room room)
    {
        if (_roomIds.Contains(room.Id))
        {
            return Error.Conflict(description: "Room already exists in gym");
        }

        if (_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);

        return Result.Success;
    }
}