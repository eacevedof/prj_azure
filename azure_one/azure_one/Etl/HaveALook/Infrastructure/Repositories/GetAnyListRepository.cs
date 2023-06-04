
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.HaveALook.Infrastructure.Repositories;

public sealed class GetAnyListRepository: AbsRepository
{

    public GetAnyListRepository(Mssql db) : base(db)
    {
    }
    
    public void Invoke(string table)
    {
        Lg.pr("GetAnyListRepository started!");
        string sqlPaginate = $"SELECT * FROM {table}";

        List<Dictionary<string, string>> result = _db.Query(sqlPaginate);
        foreach(var dict in result) 
        {
            string id = dict["id"];
            Lg.pr($"id: {id}");
        }
        Lg.pr("GetAnyListRepository finished!");
    }
 
}