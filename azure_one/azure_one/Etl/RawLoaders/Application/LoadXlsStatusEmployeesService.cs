using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsStatusEmployeesService: AbsRawService
{
    public override void Invoke()
    {
        ImpStatusEmployeesEntity ImpStatusEmployeesEntity = ImpStatusEmployeesEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", ImpStatusEmployeesEntity.PathXls), 
            ImpStatusEmployeesEntity.SheetNr, 
            ImpStatusEmployeesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                ImpStatusEmployeesEntity.Table, 
                ImpStatusEmployeesEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}