﻿using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public static class SessionErrors
{
    public static Error CannotHaveMoreReservationThanParticipants = Error.Validation(
        code: "Session.CannotHaveMoreReservationThanParticipants",
        description: "Cannot have more reservation than participants");

    public static Error CannotCancelReservationTooCloseToSession = Error.Validation(
        code: "Session.CannotCancelReservationTooCloseToSession",
        description: "Cannot cancel reservation too close to session");
}