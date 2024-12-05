namespace Features.Customers.Application.Models;

public class CustomerDetailsModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
}