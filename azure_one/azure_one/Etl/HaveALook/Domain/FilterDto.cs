using System.Collections.Generic;

namespace azure_one.Etl.HaveALook.Domain;

public sealed class FilterDto
{   
    public readonly string search;
    public readonly int page;
    public readonly int pageSize;
    public readonly string orderBy;
    public readonly string columnName;

    public FilterDto(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems

    )
    {
        totalPages = _totalPages;
        nextPage = _nextPage;
        currentPage = _currentPage;
        totalItems = _totalItems;
    }

    public static FilterDto fromPrimitives(
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems      
    )
    {
        return new FilterDto(
          _totalPages,
          _nextPage,
          _currentPage,
          _totalItems
        );
    }
}