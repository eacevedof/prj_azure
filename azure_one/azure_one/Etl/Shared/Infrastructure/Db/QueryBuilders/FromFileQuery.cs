using System.IO;
using azure_one.Etl.Shared.Infrastructure.Log;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class FromFileQuery
{
    private readonly string _pathFileSql;

    public FromFileQuery(string pathFileSql)
    {
        _pathFileSql = pathFileSql;
    }

    public static FromFileQuery fromPrimitive(string pathFileSql)
    {
        return (new(pathFileSql));
    }
    
    public void Invoke()
    {
        if (_pathFileSql.IsEmpty())
        {
            Lg.pr("no sql path file","FromFileQuery");
            return;
        }
        string sql = GetQueryContent() ?? "";
        if (sql.IsEmpty())
        {
            Lg.pr("no sql data": "FromFileQuery");
            return;
        }
        Lg.pr(sql, _pathFileSql);
        (new Mssql()).Execute(sql);
    }

    private string GetQueryContent()
    {
        return File.ReadAllText(_pathFileSql).Trim();
    }
}