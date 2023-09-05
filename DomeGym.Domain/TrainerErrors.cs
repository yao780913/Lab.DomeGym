using ErrorOr;

namespace DomeGym.Domain;

public static class TrainerErrors
{
    public static Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        "Trainer.CannotHaveTwoOrMoreOverlappingSessions",
        "Cannot have two or more overlapping sessions");
}