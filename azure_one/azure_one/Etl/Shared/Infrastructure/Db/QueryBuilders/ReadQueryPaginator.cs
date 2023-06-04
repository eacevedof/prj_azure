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
        
        return this;
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