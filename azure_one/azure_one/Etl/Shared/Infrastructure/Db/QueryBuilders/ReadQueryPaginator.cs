using System;
using System.Linq;
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQueryPaginator
{
    
    private Mssql _mssql = null;
    private ReadQuery _readQuery = null;
    private int _page = 1;
    private int _pageSize = 50;

    private int _totalRows = 0;
    private int _offsetStart = 0;
    private int _offsetPageSize = 50; //[1 - n]
    private int _totalPages = 0;

    private int _fullPages = 0;
    private int _itemsInLastPage = 0;

    private string _sql = "";
    private string _sqlCount = "";
   
    private List<Dictionary<string, string>> _rows = new List<Dictionary<string, string>>();

    public ReadQueryPaginator(ReadQuery readQuery, int page, int pageSize)
    {
        _readQuery = readQuery;
        
        _page = page;
        if (_page < 1) _page = 1;
        if (pageSize > 0) _pageSize = pageSize;

        _mssql = Mssql.GetInstanceByReq();
    }
    
    public static ReadQueryPaginator fromPrimitives(ReadQuery readQuery, int page, int pageSize)
    {
        return new ReadQueryPaginator(readQuery, page, pageSize);
    }

    public ReadQueryPaginator Calculate()
    {
        _readQuery.Select();

        _sqlCount = _readQuery.GetSqlCount();
        Lg.pr(_sqlCount, "_sqlCount");
        
        _loadTotalRows();
        if (_totalRows == 0) return this;

        _loadPages();
        _loadOffsetStart();

        _readQuery.setOffset(_offsetStart, _offsetPageSize);
        _sql = _readQuery.Select().GetSql();
        Lg.pr(_sql, "_sql");

        _loadRows();

        return this;
    }

    private void _loadTotalRows()
    {
        List<Dictionary<string, string>> resultTotal = _mssql.Query(_sqlCount);
        if (resultTotal.Count == 0) return;

        var dict = resultTotal[0];
        
        _totalRows = int.Parse(dict["total"]);
    }

    private void _loadRows()
    {
        _rows = _mssql.Query(_sql);
    }

    private void _loadPages()
    {
        if (_pageSize >= _totalRows) {
            _fullPages = 1;
            _totalPages = 1;
            _offsetStart = 0;
            _page = 1;
            _offsetPageSize = _totalRows;
            return;
        }

        _offsetPageSize = _pageSize;
        _fullPages = _totalRows / _pageSize;
        _itemsInLastPage = _totalRows % _pageSize;

        _totalPages = _fullPages;
        if (_itemsInLastPage > 0)
            _totalPages = _fullPages + 1;
        
        if (_page > _totalPages)
            _page = _totalPages;
    }

    private void _loadOffsetStart()
    {
        _offsetStart = (_page - 1) * _offsetPageSize;
    }

    public int GetTotalPages()
    {
        return _totalPages;
    }

    public int GetNextPage()
    {
        if (_page < _totalPages)
            return _page + 1;
        return _totalPages;
    }

    public List<Dictionary<string, string>> GetRows()
    {
        return _rows;
    }

    public int GetTotalCount()
    {
        return _totalRows;
    }

    public int GetCurrentPage()
    {
        return _page;
    }    
}