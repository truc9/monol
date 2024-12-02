namespace Features.WorkOrders.Domain;

public class WorkOrderTask : Entity
{
    public string Name { get; set; }
    public WorkOrderTaskType Type { get; set; }
    public bool IsCompleted { get; set; }
}