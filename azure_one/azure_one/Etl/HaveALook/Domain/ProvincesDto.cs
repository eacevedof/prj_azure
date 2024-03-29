using System.Collections.Generic;

namespace azure_one.Etl.HaveALook.Domain;


public sealed class ProvincesDto
{   
    public readonly List<Dictionary<string, string>> dataInPage;
    public readonly int totalPages;
    public readonly int currentPage;
    public readonly int nextPage;    
    public readonly int totalItems;
    public readonly int pageSize;
    public readonly int totalItemsInPage;

    public ProvincesDto(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems,
      int _pageSize,
      int _totalItemsInPage
    )
    {
        dataInPage = _dataInPage;
        totalPages = _totalPages;
        nextPage = _nextPage;
        currentPage = _currentPage;
        totalItems = _totalItems;
        pageSize = _pageSize;
        totalItemsInPage = _totalItemsInPage;
    }

    public static ProvincesDto fromPrimitives(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems,
      int _pageSize,
      int _totalItemsInPage
    )
    {
        return new ProvincesDto(
          _dataInPage,
          _totalPages,
          _nextPage,
          _currentPage,
          _totalItems,
          _pageSize,
          _totalItemsInPage
        );
    }
}