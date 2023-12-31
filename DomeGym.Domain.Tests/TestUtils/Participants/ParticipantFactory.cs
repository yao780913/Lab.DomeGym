﻿using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.Tests.TestConstants;

namespace DomeGym.Domain.Tests.TestUtils.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant (Guid? id = null, Guid? userId = null)
    {
        return new Participant(
            userId: userId ?? Constants.User.Id,
            id: id ?? Constants.Participants.Id);
    }
}