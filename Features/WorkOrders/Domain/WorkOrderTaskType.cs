namespace Features.WorkOrders.Domain;

public class WorkOrderTaskType : Entity, IAggregateRoot
{
    public string Name { get; set; }
}