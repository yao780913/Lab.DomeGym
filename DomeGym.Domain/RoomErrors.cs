using ErrorOr;

namespace DomeGym.Domain;

public static class RoomErrors
{
    public static Error CannotHaveMoreSessionThanSubscriptionAllows = Error.Validation(
        "Room.CannotHaveMoreSessionThanSubscriptionAllows",
        "A room cannot have more scheduled session than the subscription allows");

    public static Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        "Room.CannotHaveTwoOrMoreOverlappingSessions",
        "A room cannot have two or more overlapping sessions");
}