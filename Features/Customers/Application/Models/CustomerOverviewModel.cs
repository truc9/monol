namespace Features.Customers.Application.Models;

public class CustomerOverviewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Phone { get; set; }
    public required string Email { get; set; }
}