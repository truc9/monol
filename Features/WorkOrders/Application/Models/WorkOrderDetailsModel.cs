namespace Features.WorkOrders.Application.Models;

public class WorkOrderDetailsModel
{
    public Guid Id { get; set; }
    public string Reference { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public DateTime PlanStartDate { get; set; }
    public DateTime PlanEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public double Duration => (PlanEndDate - PlanStartDate).TotalDays;
}