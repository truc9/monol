namespace Share;

public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    private List<DomainEvent> _events = new();

    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

    public void AddEvent(DomainEvent @event) => _events.Add(@event);

    public void ClearEvents() => _events.Clear();
}