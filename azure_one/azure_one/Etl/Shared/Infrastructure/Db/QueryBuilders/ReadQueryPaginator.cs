using System;
using System.Linq;
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQueryPaginator
{
    
    private Mssql _mssql = null;
    private ReadQuery _readQuery = null;
    private int _page = 1;
    private int _pageSize = 1;

    private int _totalRows = 0;
    private int _offsetStart = 0;
    private int _totalPages = 0;

    private string _sql = "";
    private string _sqlCount = "";
   
    private List<Dictionary<string, string>> _rows = new List<Dictionary<string, string>>();

    public ReadQueryPaginator(ReadQuery readQuery, int page, int pageSize)
    {
        _readQuery = readQuery;
        
        _page = page;
        if (_page < 1) throw new Exception("wrong offset page");
        
        _pageSize = pageSize;
        if (_pageSize < 1) 
            throw new Exception("wrong page size");

        _mssql = Mssql.GetInstanceByReq();
    }
    
    public static ReadQueryPaginator fromPrimitives(ReadQuery readQuery, int page, int pageSize)
    {
        return new ReadQueryPaginator(readQuery, page, pageSize);
    }

    public ReadQueryPaginator Calculate()
    {
        _sqlCount = _readQuery.GetSqlCount();
        
        _loadTotalRows();
        if (_totalRows == 0) return this;

        _sql = _readQuery.GetSql();
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

    public int GetTotalPages()
    {
        return _totalPages;
    }

    public int GetNextPage()
    {
        return _page + 1;
    }

}