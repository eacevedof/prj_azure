using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsEmployeesService: AbsRawService
{
    public override void Invoke()
    {
        ImpEmployeesEntity ImpEmployeesEntity = ImpEmployeesEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", ImpEmployeesEntity.PathXls), 
            ImpEmployeesEntity.SheetNr, 
            ImpEmployeesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                ImpEmployeesEntity.Table, 
                ImpEmployeesEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}