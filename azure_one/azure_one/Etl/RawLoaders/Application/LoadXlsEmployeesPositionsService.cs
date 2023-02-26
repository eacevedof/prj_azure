using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsEmployeesPositionsService: AbsRawService
{
    public override void Invoke()
    {
        ImpEmployeesPositionsEntity ImpEmployeesPositionsEntity = ImpEmployeesPositionsEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", ImpEmployeesPositionsEntity.PathXls), 
            ImpEmployeesPositionsEntity.SheetNr, 
            ImpEmployeesPositionsEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                ImpEmployeesPositionsEntity.Table, 
                ImpEmployeesPositionsEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}