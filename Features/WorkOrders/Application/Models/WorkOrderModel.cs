namespace Features.WorkOrders.Application.Models;

public class WorkOrderModel
{
    public string Reference { get; set; }
    public string Description { get; set; }
    public DateTime PlanStartDate { get; set; }
    public DateTime PlanEndDate { get; set; }
    public Address Address { get; set; }
}