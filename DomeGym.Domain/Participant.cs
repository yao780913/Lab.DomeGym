namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new ();

    public Participant (Guid userId, Guid? id = null)
    {
        _userId = userId;
        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }
}