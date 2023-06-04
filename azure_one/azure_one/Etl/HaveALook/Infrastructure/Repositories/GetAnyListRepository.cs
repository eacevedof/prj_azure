
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;

using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

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

        ReadQuery readQuery = ReadQuery.fromTable("[imp_provinces] p");

        readQuery.SetComment("primera query");

        readQuery
            .AddGetField("p.tenant_slug")
            .AddGetField("p.countries_uuid")
            .AddGetField("p.countries_id")
            .AddGetField("MAX(p.val) province")
            //.AddGetField("p.codesap AS p_codesap")
            //.AddGetField("c.codesap AS c_codesap")
            .AddGetField("MAX(c.val) AS country")
            .AddJoin(@"INNER JOIN [imp_countries] c ON p.countries_id = c.countries_id")
            .AddWhere("p.id > 3").AddWhere("c.id > 5")
            .AddGroupBy("p.countries_uuid")
            .AddGroupBy("p.tenant_slug")
            .AddGroupBy("p.countries_id")
            .AddHaving("LENGTH(p.countries_id) > 2")
            .AddOrderBy("p.countries_uuid", "DESC")
        ;

        string sql = readQuery.Select().GetSql();
        List<Dictionary<string, string>> result = _db.Query(sqlPaginate);
        foreach(var dict in result) 
        {
            string id = dict["id"];
            Lg.pr($"id: {id}");
        }
        Lg.pr("GetAnyListRepository finished!");
    }
 
}