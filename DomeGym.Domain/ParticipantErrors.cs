using ErrorOr;

namespace DomeGym.Domain;

public static class ParticipantErrors
{
    public static Error CannotHaveTwoOrMoreOverlappingSessions = Error.Conflict(
        "Participants.CannotHaveTwoOrMoreOverlappingSessions",
        "A Participant Cannot have two or more overlapping sessions");
}