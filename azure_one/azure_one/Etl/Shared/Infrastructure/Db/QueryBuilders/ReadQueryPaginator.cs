using System;
using System.Linq;
using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQueryPaginator
{
    
    private ReadQuery _readQuery = null;
    private int _start = 0;
    private int _pageSize = 0;

    public ReadQueryPaginator(ReadQuery readQuery, int start, int pageSize)
    {
        _readQuery = readQuery;
        _start = start;
        _pageSize = pageSize;
    }
    
    

}