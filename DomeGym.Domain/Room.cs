namespace DomeGym.Domain;

public class Room
{
    private readonly Guid _id;
    private readonly List<Guid> _sessionIds;

    public Room (Guid id)
    {
        _id = id;
    }

    public Guid Id => _id;
}