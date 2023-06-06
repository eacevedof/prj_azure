using System.Collections.Generic;

namespace azure_one.Etl.HaveALook.Domain;

public sealed class FilterDto
{   
    public readonly string _search;
    public readonly int _page;
    public readonly int _pageSize;
    public readonly string _orderBy;
    public readonly string _columnName;

    public FilterDto(
      int search,
      int page,
      int pageSize,
      int orderBy
    )
    {
        this._search = search;
        this._page = page;
        this._pageSize = pageSize;
        this._orderBy = orderBy;
    }

    public static FilterDto fromPrimitives(
      int _search,
      int _page,
      int _pageSize,
      int _orderBy      
    )
    {
        return new FilterDto(
          _search,
          _page,
          _pageSize,
          _orderBy
        );
    }
}