namespace DomeGym.Domain.Common.Entities;

public class Reservation : Entity
{
    public Reservation (Guid participantId, Guid? id = null)
        : base (id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    public Guid ParticipantId { get; }
}