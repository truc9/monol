using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Share;

[NotMapped]
public class DomainEvent : INotification
{
    public Guid Id { get; set; }
}