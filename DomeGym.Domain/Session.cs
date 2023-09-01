namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds;
    
    private readonly int _maxParticipants;

    public Session (int maxParticipants, Guid? id = null)
    {
        _maxParticipants = maxParticipants;

        _id = id ?? Guid.NewGuid();
    }
}