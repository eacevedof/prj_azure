using System.Collections.Generic;

namespace azure_one.Etl.HaveALook.Domain;

public sealed class FilterDto
{   
    public readonly List<Dictionary<string, string>> dataInPage;
    public readonly int totalPages;
    public readonly int nextPage;
    public readonly int currentPage;
    public readonly int totalItems;

    public FilterDto(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems

    )
    {
        dataInPage = _dataInPage;
        totalPages = _totalPages;
        nextPage = _nextPage;
        currentPage = _currentPage;
        totalItems = _totalItems;
    }

    public static FilterDto fromPrimitives(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems      
    )
    {
        return new FilterDto(
          _dataInPage,
          _totalPages,
          _nextPage,
          _currentPage,
          _totalItems
        );
    }
}