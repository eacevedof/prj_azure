
namespace azure_one.Etl.HaveALook.Domain;

public sealed class FilterDto
{   
    private readonly string _search;
    private readonly int _page;
    private readonly int _pageSize;
    private readonly string _orderBy;
    private readonly string _columnName;

    public FilterDto(
      string search,
      int page,
      int pageSize,
      string orderBy,
      string columnName
    )
    {
        this._search = search;
        this._page = page;
        this._pageSize = pageSize;
        this._orderBy = orderBy;
        this._columnName = columnName;
    }

    public static FilterDto fromPrimitives(
      string search,
      int page,
      int pageSize,
      string orderBy,
      string columnName
    )
    {
        return new FilterDto(
          search,
          page,
          pageSize,
          orderBy,
          columnName
        );
    }

    public string search()
    {
      return _search;
    }
    public string orderBy()
    {
      return orderBy;
    }
}