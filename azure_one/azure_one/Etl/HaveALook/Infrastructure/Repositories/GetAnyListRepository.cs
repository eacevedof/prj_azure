
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.CreateImpTables.Infrastructure.Repositories;

public sealed class GetAnyListRepository: AbsRepository
{
    private readonly string _pathFilesFolder;

    public GetAnyListRepository(Mssql db) : base(db)
    {
    }
    
    public void Invoke(string table)
    {
        Lg.pr("GetAnyListRepository started!");
        List<Dictionary<string, string>> result = _db.Query($"SELECT * FROM {table}");
        foreach(var dict in result) 
        {
            string id = dict["id"];
            Lg.pr($"id: {id}");
        }
        Lg.pr("GetAnyListRepository finished!");
    }
 
}