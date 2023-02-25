using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsCountriesServices: AbsRawService
{
    public override void Invoke()
    {
        ImpCountriesEntity countries = ImpCountriesEntity.GetInstance();
        
        string pathExcel = Env.GetConcat("HOME", countries.PathXls);
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            countries.SheetNr, 
            countries.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                countries.Table, 
                countries.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}