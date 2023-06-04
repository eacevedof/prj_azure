using System;
using System.Linq;
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQueryPaginator
{
    
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
    }
    
    public static ReadQueryPaginator fromPrimitives(ReadQuery readQuery, int page, int pageSize)
    {
        return new ReadQueryPaginator(readQuery, page, pageSize);
    }


    public int GetTotalPages()
    {
        return 
    }

}