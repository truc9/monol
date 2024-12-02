namespace Features.WorkOrders.Domain;

public class WorkOrder : Entity, IAggregateRoot
{
    public string Reference { get; set; }
    public string Description { get; set; }
    public Address Address { get; set; }
    public DateTime PlanStartDate { get; set; }
    public DateTime PlanEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    private List<WorkOrderTask> _tasks = new();
    public IReadOnlyCollection<WorkOrderTask> Tasks => _tasks.AsReadOnly();

    public void AddTask(string name)
    {
        if (_tasks.Any(t => t.Name == name))
        {
            throw new DomainException($"Work order task '{name}' already exists");
        }

        _tasks.Add(new WorkOrderTask
        {
            Name = name,
        });
    }
}