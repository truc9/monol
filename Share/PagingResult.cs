namespace Share;

public class PagingResult<T> where T : class
{
    public PagingResult(IList<T> data, int pageIndex, int pageSize, int total)
    {
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Total = total;
    }

    public IList<T> Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}