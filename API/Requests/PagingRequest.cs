using System.ComponentModel;
using FastEndpoints;

namespace API.Requests;

public record PagingRequest
{
    [DefaultValue(0)]
    [QueryParam, BindFrom("pageNumber")]
    public int PageNumber { get; set; }

    [DefaultValue(25)]
    [QueryParam, BindFrom("pageSize")]
    public int PageSize { get; set; }
}