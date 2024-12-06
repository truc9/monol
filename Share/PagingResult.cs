namespace Share;

public class PagingResult<T> where T : class
{
    public PagingResult(IList<T> data, int pageNumber, int pageSize, int total)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Total = total;
    }

    public IList<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}