using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsEmployeesDepartmentsService: AbsRawService
{
    public override void Invoke()
    {
        ImpEmployeesDepartmentsEntity employeesDepartmentsEntity = ImpEmployeesDepartmentsEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", employeesDepartmentsEntity.PathXls), 
            employeesDepartmentsEntity.SheetNr, 
            employeesDepartmentsEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                employeesDepartmentsEntity.Table, 
                employeesDepartmentsEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}