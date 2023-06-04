
using System.Collections.Generic;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;

using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

using azure_one.Etl.HaveALook.Domain;

namespace azure_one.Etl.HaveALook.Infrastructure.Repositories;


public sealed class GetAnyListRepository: AbsRepository
{

    public GetAnyListRepository(Mssql db) : base(db)
    {
    }
    

    public ProvincesDto Invoke(int page, int pageSize)
    {
        ReadQuery readQuery = ReadQuery.fromTable("imp_countries c");
        readQuery
            .AddGetField("c.id c_id")
            .AddGetField("p.id p_id")
            .AddGetField("c.uuid c_uuid")
            .AddGetField("c.val country")
            .AddGetField("p.provinces_id, p.val province")
            .AddJoin("INNER JOIN imp_provinces p ON c.countries_id = p.countries_id")
            .AddWhere("c.val LIKE '%A%'")
            .AddWhere("LEN(p.val) > 1")
            .AddOrderBy("c.val")
        ;
        ReadQueryPaginator paginator = ReadQueryPaginator.fromPrimitives(
            readQuery, 
            page, 
            pageSize
        ).Calculate();
        var rows = paginator.GetRows();
        int totalPages = paginator.GetTotalPages();
        
        Lg.pr($"total pages {totalPages}");
    }

    public void Invoke_(string table)
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
            .AddHaving("LEN(p.countries_id) > 2")
            .AddOrderBy("p.countries_uuid", ReadQuery.ORDER_DESC)
            .setOffset(8, 13)
        ;

        string sql = readQuery.Select().GetSql();
        string sqlCount = readQuery.GetSqlCount();

        List<Dictionary<string, string>> result = _db.Query(sql);
        List<Dictionary<string, string>> count = _db.Query(sqlCount);

        foreach (var dict in result) 
        {
            string id = dict["id"];
            Lg.pr($"id: {id}");
        }
        Lg.pr("GetAnyListRepository finished!");
    }




}