﻿using ErrorOr;

namespace DomeGym.Domain;

public class Gym
{
    private readonly int _maxRooms;
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _roomIds = new ();

    public Gym (int maxRooms, Guid subscriptionId, Guid id)
    {
        _maxRooms = maxRooms;
        _subscriptionId = subscriptionId;
        Id = id;
    }

    public Guid Id { get; }

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