
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

        return ProvincesDto.fromPrimitives(
            paginator.GetRows(),
            paginator.GetTotalPages(),
            paginator.GetNextPage(),
            paginator.GetCurrentPage(),
            paginator.GetTotalCount(),
            paginator.GetPageSize(),
            paginator.GetTotalItemsInPage()
        );
        
    }
}