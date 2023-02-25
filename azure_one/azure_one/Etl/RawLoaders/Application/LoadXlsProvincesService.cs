using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsProvincesService: AbsRawService
{
    public override void Invoke()
    {
        ImpProvincesEntity provincesEntity = ImpProvincesEntity.GetInstance();
        string pathExcel = Env.GetConcat("HOME", provincesEntity.PathXls);
        
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            provincesEntity.SheetNr, 
            provincesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                provincesEntity.Table,
                provincesEntity.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}