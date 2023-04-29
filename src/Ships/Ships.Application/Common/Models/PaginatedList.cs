using Microsoft.EntityFrameworkCore;

namespace Ships.Application.Common.Models;

public class PaginatedList<T> : PageInfo
{
    public IReadOnlyCollection<T> Items { get; }
   

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
        ItemsPerPage = pageSize;
    }


    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
public class PageInfo
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int ItemsPerPage { get; set; }
    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;


    public int PageStart
    {
        get { return ((PageNumber - 1) * ItemsPerPage + 1); }
    }
    public int PageEnd
    {
        get
        {
            int currentTotal = (PageNumber - 1) * ItemsPerPage + ItemsPerPage;
            return (currentTotal < TotalCount ? currentTotal : TotalCount);
        }
    }
    public int LastPage
    {
        get { return (int)Math.Ceiling((decimal)TotalCount / ItemsPerPage); }
    }
}
