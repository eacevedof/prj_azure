using azure_one.Etl.HaveALook.Application;
using System.Collections.Generic;

namespace azure_one.Etl.HaveALook.Domain;

public sealed class ProvincesDto
{
    
    public readonly List<Dictionary<string, string>> dataInPage;
    public readonly int totalPages;
    public readonly int nextPage;
    public readonly int currentPage;
    public readonly int totalItems;

    public ProvincesDto(
      List<Dictionary<string, string>> _dataInPage,
      int _totalPages,
      int _nextPage,
      int _currentPage,
      int _totalItems

    )
    {
        dataInPage = _dataInPage;
        nextPage = _nextPage;
        currentPage = _currentPage;
        totalItems = _totalItems;
    }

}