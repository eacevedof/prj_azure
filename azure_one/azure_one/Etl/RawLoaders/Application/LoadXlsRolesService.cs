using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsRolesService: AbsRawService
{
    public override void Invoke()
    {
        ImpRolesEntity ImpRolesEntity = ImpRolesEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", ImpRolesEntity.PathXls), 
            ImpRolesEntity.SheetNr, 
            ImpRolesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                ImpRolesEntity.Table, 
                ImpRolesEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}