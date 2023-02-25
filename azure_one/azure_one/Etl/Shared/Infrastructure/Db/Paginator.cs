using System;
using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db;

public sealed class Paginator
{
    public static List<Tuple<int, int>> GetPages(int numItems, int pageSize)
    {
        if (numItems <= 0) return null;
        if (pageSize <= 0)
            return new List<Tuple<int, int>>
            {
                new(0, numItems - 1)
            };
        int numPages = numItems / pageSize;
        int inLastPage = numItems % pageSize;
        int withResidual = inLastPage > 0 ? numPages + 1 : numPages;

        List<Tuple<int, int>> result = new ();
        int pageSizeMinus1 = pageSize - 1;
        for(int page=0; page<=(withResidual-1); page++)
            result.Add(new Tuple<int, int>(page * pageSize, page + pageSizeMinus1));
        
        return result;
    }
}